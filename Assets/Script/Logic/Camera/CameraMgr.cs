using UnityEngine;

namespace Script.Logic
{
    public class CameraMgr
    {
        public static Camera UICamera;
        public static Camera MainCamera;

        private static Transform target;
        private static bool following;
        private static readonly Vector3 cameraRelativePos = new Vector3(0, 8, -20);//摄像机相对target的位置

        public static void Init()
        {
            MainCamera.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
        }

        public static void LookTarget(Transform t)
        {
            target = t;
            RefreshCameraPos();
        }
        
        public static void FollowTarget(Transform t)
        {
            LookTarget(t);
            following = true;
        }

        public static void StopFollow()
        {
            following = false;
        }

        public static void LateUpdate()
        {
            if (target != null && following)
            {
                RefreshCameraPos();
            }
        }

        private static void RefreshCameraPos()
        {
            var pos = target.position + cameraRelativePos;
            MainCamera.transform.position = pos;
            MapMgr.SetCenter(pos);
        }
    }
}