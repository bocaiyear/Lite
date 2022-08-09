using UnityEngine;

namespace Script.Logic
{
    public class CameraMgr
    {
        public static Camera UICamera;
        public static Camera MainCamera;

        private static Transform target;

        private static readonly Vector3 cameraRelativePos = new Vector3(0, 25, -80);//摄像机相对target的位置

        public static void Init()
        {
            MainCamera.transform.localRotation = Quaternion.Euler(new Vector3(22, 0, 0));
        }

        public static void FollowTarget(Transform t)
        {
            target = t;
        }

        public static void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            MainCamera.transform.localPosition = target.localPosition + cameraRelativePos;
        }
    }
}