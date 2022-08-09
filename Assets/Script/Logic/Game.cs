
using System;
using Script.Logic;
using UnityEngine;

[Serializable]
public class Game : Lite
{
    [SerializeField]
    public Transform target;
    void Start()
    {
        MapMgr.Init();
        CameraMgr.Init();
        PlayerMgr.Init();
    }
}
