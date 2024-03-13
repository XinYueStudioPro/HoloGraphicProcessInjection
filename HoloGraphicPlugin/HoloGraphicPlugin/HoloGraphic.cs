using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoloGraphicPlugin
{
    public class HoloGraphic 
    {
     
        public static UnityEngine.GameObject Root;
        public static void Load()
        {
          
            Root = new UnityEngine.GameObject();
            Root.AddComponent<CameraRay>();
            UnityEngine.Object.DontDestroyOnLoad(Root);
        }

        public static void Unload()
        {
            UnityEngine.Object.Destroy(Root);
        }
    }
}
