
using System;
using Script.Logic;
using Script.Resource;
using UnityEngine;

[Serializable]
public class Test : Lite
{
    void Start()
    {
        //scene
        GameObject cube = ResourceMgr.LoadAsset<GameObject>("Assets/Res/Model/Cube/Cube.prefab");
        cube = Instantiate(cube, Lite.SceneRoot);
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localRotation = Quaternion.Euler(Vector3.zero);
        cube.transform.localScale = new Vector3(100, 0.1f, 100);
        
        ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Sphere/Sphere.prefab", null);//for test
        ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Sphere/Sphere.prefab", sphere =>
        {
            sphere = Instantiate(sphere, SceneRoot);
            sphere.transform.localPosition = new Vector3(0, 10, 0);
            sphere.transform.localScale = new Vector3(10, 10, 10);
        });

        CameraMgr.MainCamera.transform.localPosition = new Vector3(0, 25, -80);
        CameraMgr.MainCamera.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));

        //ui
        ResourceMgr.LoadAsset<GameObject>("Assets/Res/UI/Main/Win_Main.prefab");//for test
        GameObject uiMain = ResourceMgr.LoadAsset<GameObject>("Assets/Res/UI/Main/Win_Main.prefab");
        uiMain = Instantiate(uiMain, UIRoot);
        Canvas canvas = uiMain.transform.GetComponent<Canvas>();
        canvas.worldCamera = CameraMgr.UICamera;
    }
}
