using Script.Resource;
using UnityEngine;

namespace Script.Logic
{
    public class PlayerMgr
    {
        public static void Init()
        {
            ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Player/Player.prefab", o =>
            {
                GameObject player = Object.Instantiate(o, Lite.SceneRoot);
                player.AddComponent<Player>();
                player.transform.position = Vector3.zero;
                CameraMgr.LookTarget(player.transform);
            });
        }
    }
}