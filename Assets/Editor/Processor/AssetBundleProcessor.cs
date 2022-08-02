using System;
using System.IO;
using Script.Resource;
using UnityEditor;

namespace Processor
{
    public class AssetBundleProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (var assetPath in importedAssets)
            {
                if (Directory.Exists(assetPath))
                {
                    continue;
                }
                AssetImporter importer = AssetImporter.GetAtPath(assetPath);
                string bundleName = ResourceMgr.PathToBundleName(assetPath);
                if (!string.IsNullOrEmpty(bundleName))
                {
                    importer.assetBundleName = bundleName;
                }
            }
        }
    }
}