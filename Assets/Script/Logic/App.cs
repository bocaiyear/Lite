
using Script.Resource;
using UnityEngine;

public class App : MonoBehaviour
{
    public static Camera UICamera;
    public static Camera MainCamera;

    public static Transform UIRoot;
    public static Transform SceneRoot;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        UIRoot = UICamera.transform.parent;
        SceneRoot = MainCamera.transform.parent;
        
        //scene
        GameObject cube = ResourceMgr.LoadAsset<GameObject>("Assets/Res/Model/Cube/Cube.prefab");
        cube = Instantiate(cube, SceneRoot);
        cube.transform.parent = SceneRoot;
        cube.transform.localPosition = new Vector3(-1, -1, 20);
        
        //ui
        GameObject uiMain = ResourceMgr.LoadAsset<GameObject>("Assets/Res/UI/Main/UIMain.prefab");
        uiMain = Instantiate(uiMain, UIRoot);
        Canvas canvas = uiMain.transform.GetComponent<Canvas>();
        canvas.worldCamera = UICamera;
    }
}
