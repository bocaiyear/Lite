
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace Menu
{
    public class SpriteMenu
    {
        [MenuItem("Assets/Lite/Create Atlas")]
        private static void CreateAtlasByFolder()
        {
            var objects = Selection.objects;
            if (objects.Length == 0)
            {
                Debug.LogWarning("请选择一个目录");
                return;
            }
            foreach (var obj in objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                if (!path.StartsWith(Const.UI_ROOT_PATH))
                {
                    Debug.LogWarning($"必须在路径{Const.UI_ROOT_PATH}下");
                    return;
                }

                if (!path.EndsWith(Const.UI_ATLAS_PATH))
                {
                    Debug.LogWarning($"必须是目录{Const.UI_ATLAS_PATH}");
                    return;
                }
                
                SpriteAtlas atlas = new SpriteAtlas();
                var packingSettings = atlas.GetPackingSettings();
                packingSettings.padding = 8;
                packingSettings.enableTightPacking = false;
                packingSettings.enableRotation = false;
                atlas.SetPackingSettings(packingSettings);
                SetTextureFormatAndroid(atlas);
                SetTextureFormatIOS(atlas);
                
                atlas.Add(new []{obj});
                atlas.SetIncludeInBuild(false);
                
                string folderPath = Path.GetDirectoryName(path);
                string fileName = path.Substring(Const.UI_ROOT_PATH.Length);
                fileName = fileName.Replace("/", "_");
                AssetDatabase.CreateAsset(atlas, folderPath + "/" + fileName + Const.ATLAS_FILE_SUFFIX);
                Debug.Log($"创建了图集{fileName}");
            }
            AssetDatabase.SaveAssets(); //必须要，否者不会生成
        }

        private static void SetTextureFormatAndroid(SpriteAtlas atlas)
        {
            var platformSettings = atlas.GetPlatformSettings(Const.ANDROID);
            platformSettings.overridden = true;
            platformSettings.format = TextureImporterFormat.ASTC_6x6;
            atlas.SetPlatformSettings(platformSettings);
        }
        
        private static void SetTextureFormatIOS(SpriteAtlas atlas)
        {
            var platformSettings = atlas.GetPlatformSettings(Const.IOS);
            platformSettings.overridden = true;
            platformSettings.format = TextureImporterFormat.ASTC_6x6;
            atlas.SetPlatformSettings(platformSettings);
        }
    }
}