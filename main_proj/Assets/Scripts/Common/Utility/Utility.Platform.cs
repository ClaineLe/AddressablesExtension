using System.Collections;
using UnityEngine;


namespace com.halo.framework
{
    namespace common
    {
        public static partial class Utility
        {

            public class Platform
            {

                public static string GetPlatformName()
                {
#if UNITY_EDITOR
                    switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
                    {
                        case UnityEditor.BuildTarget.Android:
                            return "Android";
                        case UnityEditor.BuildTarget.iOS:
                            return "iOS";
                        case UnityEditor.BuildTarget.StandaloneWindows:
                        case UnityEditor.BuildTarget.StandaloneWindows64:
                            return "Windows";
                        case UnityEditor.BuildTarget.StandaloneOSX:
                            return "OSX";
                        default:
                            return null;
                    }
#else
              switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return "Android";
                    case RuntimePlatform.IPhonePlayer:
                        return "iOS";
                    case RuntimePlatform.WindowsPlayer:
                        return "Windows";
                    case RuntimePlatform.OSXPlayer:
                        return "OSX";
                    default:
                        return null;
                }
#endif
                }
            }
        }
    }
}