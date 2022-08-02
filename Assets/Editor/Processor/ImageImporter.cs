
using UnityEditor;
using UnityEngine;

namespace Processor
{
    public class ImageImporter : AssetPostprocessor
    {
        private void OnPostprocessTexture(Texture2D texture)
        {
            TextureImporter textureImporter = assetImporter as TextureImporter;
            if (assetPath.StartsWith(Const.UI_ROOT_PATH))
            {
                string relativePath = assetPath.Substring(Const.UI_ROOT_PATH.Length);
                if (relativePath.Contains(Const.UI_ATLAS_PATH))
                {
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.spriteImportMode = SpriteImportMode.Single;
                    textureImporter.mipmapEnabled = false;
                    SetTextureFormatAndroid(textureImporter);
                    SetTextureFormatIOS(textureImporter);
                    AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                }
            }
        }

        private static void SetTextureFormatAndroid(TextureImporter textureImporter)
        {
            var settings = textureImporter.GetPlatformTextureSettings(Const.ANDROID);
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            textureImporter.SetPlatformTextureSettings(settings);
        }

        private static void SetTextureFormatIOS(TextureImporter textureImporter)
        {
            var settings = textureImporter.GetPlatformTextureSettings(Const.IOS);
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            textureImporter.SetPlatformTextureSettings(settings);
        }
    }
}