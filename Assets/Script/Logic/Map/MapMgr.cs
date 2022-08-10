
using Script.Resource;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Logic
{
    public class MapMgr
    {
        private static GameObject[] cubes = new GameObject[9];
        
        private static readonly float WIDTH = 100;
        private static readonly Vector3[,] LAYOUT = new Vector3[3, 3];

        public static void Init()
        {
            LAYOUT[0, 0] = new Vector3(-WIDTH, 0, WIDTH);
            LAYOUT[0, 1] = new Vector3(0, 0, WIDTH);
            LAYOUT[0, 2] = new Vector3(WIDTH,0, WIDTH);
            LAYOUT[1, 0] = new Vector3(-WIDTH,0, 0);
            LAYOUT[1, 1] = new Vector3(0,0, 0);
            LAYOUT[1, 2] = new Vector3(WIDTH,0, 0);
            LAYOUT[2, 0] = new Vector3(-WIDTH,0, -WIDTH);
            LAYOUT[2, 1] = new Vector3(0,0, -WIDTH);
            LAYOUT[2, 2] = new Vector3(WIDTH,0, -WIDTH);
            
            ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Cube/Cube.prefab", o =>
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        GameObject cube = Object.Instantiate(o, Lite.SceneRoot);
                        cube.isStatic = true;
                        cube.transform.position = LAYOUT[i, j];
                        cubes[i * 3 + j] = cube;

                        cube.AddComponent<BoxCollider>();
                        EventTrigger trigger = cube.AddComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback.AddListener(OnClick);
                        trigger.triggers.Add(entry);
                    }
                }
            });
        }

        public static void SetCenter(Vector3 center)
        {
            Debug.Log(center);
        }

        public static void OnClick(BaseEventData arg)
        {
            PointerEventData eventData = arg as PointerEventData;
            Ray ray = CameraMgr.MainCamera.ScreenPointToRay(eventData.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var worldPos = new Vector3(hit.point.x, 0, hit.point.z);
                Player.Instance.MoveTo(worldPos);
            }
        }
    }
}