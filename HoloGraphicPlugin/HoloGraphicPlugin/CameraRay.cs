
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
namespace HoloGraphicPlugin
{
    public class CameraRay : UnityEngine.MonoBehaviour
    {


        GameObject sphere;


        Vector3 hit;
        private void OnGUI()
        {
            UnityEngine.GUI.Label(new UnityEngine.Rect(50, 10, 500, 200), "HoloGraphicPlugin hit:" + hit.ToString());
 
        }

        public void Start()
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(10f, 10f, 10f);

            sphere.transform.parent = HoloGraphic.Root.transform;


        }
        public void Update()
        {
            Vector3 origin = Camera.main.transform.position;
            Vector3 normalized = Camera.main.transform.forward.normalized;
            Vector3 vector = origin + normalized * 50000;
            Segment3 ray = new Segment3(origin, vector);


            if (Singleton<TerrainManager>.instance.RayCast(ray, out hit))
            {
                sphere.transform.position = hit;

            }
        }
    }
}
