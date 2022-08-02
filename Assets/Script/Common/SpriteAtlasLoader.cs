using Script.Resource;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace Script.Common
{
    public class SpriteAtlasLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init()
        {
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        }

        private static void OnAtlasRequested(string tag, System.Action<SpriteAtlas> callback)
        {
            if (!tag.EndsWith(Const.ATLAS_FILENAME_SUFFIX))
            {
                Debug.LogWarning("不符合标准的图集tag:" + tag);
                return;
            }

            string path = tag.Substring(0, tag.Length - Const.ATLAS_FILENAME_SUFFIX.Length);
            path = path.Replace("_", "/");
            SpriteAtlas atlas = ResourceMgr.LoadAsset<SpriteAtlas>(Const.UI_ROOT_PATH + path + "/" + tag + Const.ATLAS_FILE_SUFFIX);
            callback(atlas);
        }
    }
}