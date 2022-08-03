using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.Resource
{
    public class ResourceMgr
    {
        private static HashSet<int> loadingAssets = new HashSet<int>();
        private static Dictionary<int, Object> assetsCache = new Dictionary<int, Object>();
        
        public static T LoadAsset<T>(string assetPath) where T : Object
        {
            Object asset;
            int hash = assetPath.GetAssetPathHashCode();
            if (assetsCache.TryGetValue(hash, out asset))
            {
                return asset as T;
            }
#if UNITY_EDITOR
            if (App.Instance.EditerUseBundle)
            {
                asset = LoadFromBundle<T>(assetPath);
            }
            else
            {
                asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
            }

#else
            asset = LoadFromBundle<T>(assetPath);
#endif
            assetsCache[hash] = asset;
            return asset as T;
        }

        private static T LoadFromBundle<T>(string assetPath) where T : Object
        {
            string bundleName = PathToBundleName(assetPath);
            AssetBundle assetBundle = AssetBundleMgr.LoadAssetBundle(bundleName);
            return assetBundle.LoadAsset<T>(assetPath);
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
        
        public static void LoadAssetAsync<T>(string assetPath, Action<T> callback) where T : Object
        {
            int hash = assetPath.GetAssetPathHashCode();
            Object asset;
            if (assetsCache.TryGetValue(hash, out asset))
            {
                callback?.Invoke(asset as T);
                return;
            }
            App.Instance.StartCoroutine(LoadAsync<T>(assetPath, hash, callback));
        }

        private static IEnumerator LoadAsync<T>(string assetPath, int hash, Action<T> callback) where T : Object
        {
#if UNITY_EDITOR
            if (App.Instance.EditerUseBundle)
            {
                yield return LoadFromBundleAsync(assetPath, hash, callback);    
            }
            else
            {
                Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
                assetsCache[hash] = asset;
                callback?.Invoke(asset as T);
            }
#else
            yield return LoadFromBundleAsync(assetPath, hash, callback);
#endif
        }

        private static IEnumerator LoadFromBundleAsync<T>(string assetPath, int hash, Action<T> callback) where T : Object
        {
            while (loadingAssets.Contains(hash))
            {
                yield return null;
            }
            
            Object asset;
            if (assetsCache.TryGetValue(hash, out asset))
            {
                callback?.Invoke(asset as T);
                yield break;
            }
            
            string bundleName = PathToBundleName(assetPath);
            loadingAssets.Add(hash);
            AssetBundle assetBundle = null;
            yield return AssetBundleMgr.LoadAssetBundleAsync(bundleName, bundle =>
            {
                assetBundle = bundle;
            });

            while (assetBundle == null)
            {
                yield return null;
            }
            var assetRequest = assetBundle.LoadAssetAsync(assetPath);
            yield return assetRequest;
            asset = assetRequest.asset;
            assetsCache[hash] = asset;
            loadingAssets.Remove(hash);
            callback?.Invoke(asset as T);
        }
    }

    static class R
    {
        public static int GetAssetPathHashCode(this string assetPath)
        {
            return assetPath.ToLower().GetHashCode();
        }
    }
}