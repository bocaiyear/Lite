
using System;
using Script.Resource;
using UnityEngine;

[Serializable]
public class App : MonoBehaviour
{
    [SerializeField]
    public bool EditerUseBundle;
    public static Camera UICamera;
    public static Camera MainCamera;

    public static Transform UIRoot;
    public static Transform SceneRoot;

    public static App Instance;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        UIRoot = UICamera.transform.parent;
        SceneRoot = MainCamera.transform.parent;
        
        //scene
        GameObject cube = ResourceMgr.LoadAsset<GameObject>("Assets/Res/Model/Cube/Cube.prefab");
        cube = Instantiate(cube, SceneRoot);
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localRotation = Quaternion.Euler(Vector3.zero);
        cube.transform.localScale = new Vector3(100, 0.1f, 100);
        
        ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Sphere/Sphere.prefab", sphere =>
        {
            sphere = Instantiate(sphere, SceneRoot);
            sphere.transform.localPosition = new Vector3(0, 10, 0);
            sphere.transform.localScale = new Vector3(10, 10, 10);
        });

        MainCamera.transform.localPosition = new Vector3(0, 25, -80);
        MainCamera.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));

        //ui
        GameObject uiMain = ResourceMgr.LoadAsset<GameObject>("Assets/Res/UI/Main/UIMain.prefab");
        uiMain = Instantiate(uiMain, UIRoot);
        Canvas canvas = uiMain.transform.GetComponent<Canvas>();
        canvas.worldCamera = UICamera;
    }
}
