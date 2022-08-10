
using System;
using Script.Logic;

[Serializable]
public class Game : Lite
{
    void Start()
    {
        MapMgr.Init();
        CameraMgr.Init();
        PlayerMgr.Init();
    }
}
