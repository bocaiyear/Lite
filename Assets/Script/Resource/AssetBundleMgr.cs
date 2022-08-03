using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.Resource
{
    public class AssetBundleMgr
    {
        private static AssetBundleManifest mainFest;
        private static Dictionary<string, AssetBundle> bundlesCache = new Dictionary<string, AssetBundle>();

        public static AssetBundle LoadAssetBundle(string bundleName)
        {
            AssetBundle assetBundle;
            if (bundlesCache.TryGetValue(bundleName, out assetBundle))
            {
                return assetBundle;
            }
            
            string[] dependents = Mainfest.GetDirectDependencies(bundleName);
            foreach (var dependent in dependents)
            {
                LoadAssetBundle(dependent);
            }
            
            string bundlePath = GetAssetBundlePath(bundleName);
            assetBundle = AssetBundle.LoadFromFile(bundlePath);
            bundlesCache[bundleName] = assetBundle;
            return assetBundle;
        }
        
        public static IEnumerator LoadAssetBundleAsync(string bundleName, Action<AssetBundle> callback = null)
        {
            string[] dependents = Mainfest.GetDirectDependencies(bundleName);
            foreach (var dependent in dependents)
            {
                yield return LoadAssetBundleAsync(dependent);
            }

            AssetBundle assetBundle;
            if (bundlesCache.TryGetValue(bundleName, out assetBundle))
            {
                callback?.Invoke(assetBundle);
                yield break;
            }
            
            string bundlePath = GetAssetBundlePath(bundleName);
            var bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return bundleRequest;

            assetBundle = bundleRequest.assetBundle;
            bundlesCache[bundleName] = assetBundle;
            callback?.Invoke(assetBundle);
        }
        
        private static string GetAssetBundlePath(string bundleName)
        {
            return Path.Combine(Application.streamingAssetsPath, Const.ASSET_BUNDLE_DIR, bundleName);
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
    }
}