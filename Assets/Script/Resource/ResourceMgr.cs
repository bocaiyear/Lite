using System.IO;
using UnityEngine;

namespace Script.Resource
{
    public class ResourceMgr
    {
        private static AssetBundleManifest mainFest;
        public static T LoadAsset<T>(string assetPath) where T : Object
        {
            Object asset;
#if UNITY_EDITOR
            // asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
            asset = LoadFromBundle<T>(assetPath); //for test
#else
            asset = LoadFromBundle<T>(assetPath);
#endif
            return asset as T;
        }
        
        private static string GetAssetBundlePath(string bundleName)
        {
            return Path.Combine(Application.streamingAssetsPath, Const.ASSET_BUNDLE_DIR, bundleName);
        }
    
        private static T LoadFromBundle<T>(string assetPath) where T : Object
        {
            string bundleName = PathToBundleName(assetPath);
            AssetBundle assetBundle = LoadAssetBundle(bundleName);
            return assetBundle.LoadAsset<T>(assetPath);
        }

        private static AssetBundle LoadAssetBundle(string bundleName)
        {
            string[] dependents = Mainfest.GetDirectDependencies(bundleName);
            foreach (var dependent in dependents)
            {
                LoadAssetBundle(dependent);
            }
            string bundlePath = GetAssetBundlePath(bundleName);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(bundlePath);
            return assetBundle;
        }

        private static AssetBundleManifest Mainfest
        {
            get
            {
                if (mainFest != null)
                {
                    return mainFest;
                }

                AssetBundle ab = AssetBundle.LoadFromFile(GetAssetBundlePath(Const.ASSET_BUNDLE_DIR));
                mainFest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                return mainFest;
            }
        }

        public static string PathToBundleName(string assetPath)
        {
            string bundleName = "";
            if (assetPath.StartsWith(Const.UI_ROOT_PATH))
            {
                string ext = Path.GetExtension(assetPath);
                string prefix = "";
                if (ext.Equals(Const.ATLAS_FILE_SUFFIX))
                {
                    prefix = "ui_atlas";
                } 
                else if (ext.Equals(Const.PREFAB_FILE_SUFFIX))
                {
                    prefix = "ui_prefab";
                }

                if (!string.IsNullOrEmpty(prefix))
                {
                    string folderPath = Path.GetDirectoryName(assetPath);
                    string relativePath = folderPath.Substring(Const.UI_ROOT_PATH.Length);
                    string fileName = Path.GetFileNameWithoutExtension(assetPath).ToLower();
                    string dirs = relativePath.Replace("/", "_").ToLower();
                    bundleName = $"{prefix}_{dirs}_{fileName}";
                }
            }
            else if (assetPath.StartsWith(Const.MODEL_ROOT_PATH))
            {
                string ext = Path.GetExtension(assetPath);
                string prefix = "";
                if (ext.Equals(Const.MAT_FILE_SUFFIX))
                {
                    prefix = "model_mat";
                } 
                else if (ext.Equals(Const.PREFAB_FILE_SUFFIX))
                {
                    prefix = "model_prefab";
                }
                else
                {
                    prefix = "model_texture";
                }
                string folderPath = Path.GetDirectoryName(assetPath);
                string relativePath = folderPath.Substring(Const.MODEL_ROOT_PATH.Length);
                string dirs = relativePath.Replace("/", "_").ToLower();
                bundleName = $"{prefix}_{dirs}";
            }

            return bundleName;
        }
    }
}