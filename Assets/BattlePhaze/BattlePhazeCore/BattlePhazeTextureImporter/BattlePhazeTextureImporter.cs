using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BattlePhaze.TextureImport.Handler
{
    public class BattlePhazeTextureImporter : MonoBehaviour
    {
        public TextureWrapMode WrapMode = TextureWrapMode.Clamp;
        public MaxTextureSize TextureSize = MaxTextureSize.X2048;
        public TextureImporterType TextureType = TextureImporterType.Default;
        public bool ModifyWrapMode;
        public bool ModifyTextureType;
        public bool ModifyMaxTextureSize;

        public void TexturePropertyModification(string path)
        {
            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            importer.wrapMode = WrapMode;
            importer.textureType = TextureType;
            importer.maxTextureSize = ResolveMaxTexureSize();
            importer.SaveAndReimport();
        }
        /*
        public void ImportAsset(string path)
        {
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        */
        /// <summary>
        /// max Texture Size
        /// </summary>
        public enum MaxTextureSize
        {
            X64, X128, X256, X512, X1024, X2048, X4096
        };
        /// <summary>
        /// Resolve max texture size
        /// </summary>
        /// <returns></returns>
        public int ResolveMaxTexureSize()
        {
            switch (TextureSize)
            {
                case MaxTextureSize.X64:
                    return 64;
                case MaxTextureSize.X128:
                    return 128;
                case MaxTextureSize.X256:
                    return 256;
                case MaxTextureSize.X512:
                    return 512;
                case MaxTextureSize.X1024:
                    return 1024;
                case MaxTextureSize.X2048:
                    return 2048;
                case MaxTextureSize.X4096:
                    return 4096;
            }
            return 1024;
        }
    }
}