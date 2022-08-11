
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Processor
{
    public class ImageImporter : AssetPostprocessor
    {
        private void OnPostprocessTexture(Texture2D texture)
        {
            TextureImporter textureImporter = assetImporter as TextureImporter;
            if (assetPath.StartsWith(Const.UI_ROOT_PATH) && assetPath.Contains(Const.UI_ATLAS_PATH))
            {
                textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.spriteImportMode = SpriteImportMode.Single;
                    textureImporter.mipmapEnabled = false;
                    SetTextureFormatAndroid(textureImporter);
                    SetTextureFormatIOS(textureImporter);
                    AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            } 
            else if (assetPath.StartsWith(Const.MODEL_ROOT_PATH) && assetPath.Contains(Const.MODEL_TEXTURE_PATH))
            {
                string fileName = Path.GetFileNameWithoutExtension(assetPath);
                textureImporter.textureType = TextureImporterType.Default;
                if (fileName.EndsWith(Const.NORMAL_MAP_NAME_FLAG))
                {
                    textureImporter.textureType = TextureImporterType.NormalMap;
                }
                
                textureImporter.textureShape = TextureImporterShape.Texture2D;
                textureImporter.mipmapEnabled = true;
                SetTextureFormatAndroid(textureImporter, TextureImporterFormat.ASTC_6x6);
                SetTextureFormatIOS(textureImporter, TextureImporterFormat.ASTC_6x6);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }

        private static void SetTextureFormatAndroid(TextureImporter textureImporter, TextureImporterFormat format = TextureImporterFormat.RGBA32)
        {
            var settings = textureImporter.GetPlatformTextureSettings(Const.ANDROID);
            settings.overridden = true;
            settings.format = format;
            textureImporter.SetPlatformTextureSettings(settings);
        }

        private static void SetTextureFormatIOS(TextureImporter textureImporter, TextureImporterFormat format = TextureImporterFormat.RGBA32)
        {
            var settings = textureImporter.GetPlatformTextureSettings(Const.IOS);
            settings.overridden = true;
            settings.format = format;
            textureImporter.SetPlatformTextureSettings(settings);
        }
    }
}