using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.halo.framework
{
    namespace common
    {
        public static partial class Utility
        {
            public class AssetBundle
            {
                static public string AssetRootFoldPath = "Assets/ResExport/";
                public enum Mode
                {
                    Together,
                    Separately,
                }

                static public string AssetPathToAssetBundleName(string assetPath)
                {
                    return System.IO.Path.GetDirectoryName(assetPath);
                }

                static public string AssetPathToBundleName(string path)
                {
                    string bundleName = path;
                    if (System.IO.Path.HasExtension(path))
                    {
                        string extension = System.IO.Path.GetExtension(path);
                        bundleName = bundleName.Replace(extension, string.Empty);
                    }
                    bundleName = bundleName.Replace(AssetRootFoldPath, string.Empty);
                    return bundleName.Replace("\\", "_").Replace("/", "_").ToLower();
                }
            }
        }
    }
}