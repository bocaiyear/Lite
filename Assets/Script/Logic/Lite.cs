
using System;
using UnityEngine;

namespace Script.Logic
{
    public class Lite : MonoBehaviour
    {
        [SerializeField]
        public bool EditerUseBundle;
        
        

        public static Transform UIRoot;
        public static Transform SceneRoot;

        public static Lite Instance;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        
            CameraMgr.UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
            CameraMgr.MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            UIRoot = CameraMgr.UICamera.transform.parent;
            SceneRoot = CameraMgr.MainCamera.transform.parent;
        }

        private void LateUpdate()
        {
            CameraMgr.LateUpdate();
        }
    }
}