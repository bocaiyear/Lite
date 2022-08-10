
using Script.Resource;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Logic
{
    public class MapMgr
    {
        public static void Init()
        {
            ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Cube/Cube.prefab", o =>
            {
                GameObject cube = Object.Instantiate(o, Lite.SceneRoot);
                cube.isStatic = true;
                cube.transform.position = Vector3.zero;

                cube.AddComponent<BoxCollider>();
                EventTrigger trigger = cube.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener(OnClick);
                trigger.triggers.Add(entry);
            });
        }

        public static void SetCenter(Vector3 center)
        {
            
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