using SharpMonoInjector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ProcessInjection : MonoBehaviour
{
    string AssemblyPath = Path.Combine(Application.streamingAssetsPath, "HoloGraphicPlugin.dll");
      Injector mInjector;
      IntPtr HoloGraphicPlugin= IntPtr.Zero;

    public string ProcessName = "cities";
    private void Load()
    {
        Process[] pname = Process.GetProcesses();

        foreach (Process name in pname)
        {
            //UnityEngine.Debug.Log("Process Id: " + name.Id+ " ProcessName : " + name.ProcessName);
            
            if (name.ProcessName.ToLower().IndexOf(ProcessName) != -1)
            {
                mInjector = new Injector(name.Id);
                byte[] file;
                try
                {
                    file = File.ReadAllBytes(AssemblyPath);
                }
                catch (IOException)
                {
                    UnityEngine.Debug.Log("Failed to read the file " + AssemblyPath);
                    return;
                }
                HoloGraphicPlugin = mInjector.Inject(file, "HoloGraphicPlugin", "HoloGraphic", "Load");
                break;
            }
        }

        if (mInjector == null || HoloGraphicPlugin == IntPtr.Zero)
        {
            UnityEngine.Debug.Log("Failed to Inject the  " + AssemblyPath);
        }
    }

    private void UnLoad()
    {
        if (mInjector != null && HoloGraphicPlugin != IntPtr.Zero)
        {
            mInjector.Eject(HoloGraphicPlugin, "HoloGraphicPlugin", "HoloGraphic", "Unload");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        UnLoad();
    }
}
