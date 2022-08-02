using System.IO;
using Editor;
using UnityEditor;
using UnityEngine;

namespace Menu
{
    public class AssetBundleMenu
    {
        [MenuItem("Lite/AssetBundle/Create AssetBundle")]
        public static void CreateAssetBundle()
        {
            BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression |
                                              BuildAssetBundleOptions.StrictMode |
                                              BuildAssetBundleOptions.DeterministicAssetBundle;
            string path = Path.Combine(Application.streamingAssetsPath, Const.ASSET_BUNDLE_DIR);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            BuildPipeline.BuildAssetBundles(path, options, EditorUserBuildSettings.activeBuildTarget);
            Debug.Log($"创建了{EditorUserBuildSettings.activeBuildTarget} AssetBundle");
        }

        [MenuItem("Lite/AssetBundle/Clear AssetBundle")]
        public static void CleanAssetBundle()
        {
            string path = Path.Combine(Application.streamingAssetsPath, Const.ASSET_BUNDLE_DIR);
            if (Directory.Exists(path))
            {
                Util.ClearDirectory(path);
            }
            Debug.Log("清空了AssetBundle");
        }
    }
}