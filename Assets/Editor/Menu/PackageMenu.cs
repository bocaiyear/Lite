using System.IO;
using Boo.Lang;
using Editor;
using UnityEditor;
using UnityEngine;

namespace Menu
{
    public class PackageMenu
    {
        [MenuItem("Lite/Create Package")]
        public static void CreateAssetBundle()
        {
            AssetBundleMenu.CleanAssetBundle();
            AssetBundleMenu.CreateAssetBundle();

            string buildPath = "";
            BuildOptions options = BuildOptions.CompressWithLz4HC;
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            if (buildTarget == BuildTarget.Android)
            {
                buildPath = Path.Combine(Util.GetBuildDir(), "Lite.apk");
            }
            else if (buildTarget == BuildTarget.StandaloneWindows || buildTarget == BuildTarget.StandaloneWindows64)
            {
                buildPath = Path.Combine(Util.GetBuildDir(), "Lite.exe");
            }

            if (string.IsNullOrEmpty(buildPath))
            {
                Debug.LogError($"不支持的平台{buildTarget}");
                return;
            }
            
            BuildPipeline.BuildPlayer(GetBuildScenes(), buildPath, buildTarget, options);
            Debug.Log($"{buildTarget}打包到了目录{buildPath}");
        }
        
        public static string[] GetBuildScenes()
        {
            List<string> scenes = new List<string>();
            foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
                if (e.enabled)
                    scenes.Add(e.path);

            return scenes.ToArray();
        }
    }
}