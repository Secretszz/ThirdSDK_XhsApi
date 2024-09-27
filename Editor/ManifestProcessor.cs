// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		ManifestProcessor.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/09/02 19:17:32
// *******************************************

#if UNITY_ANDROID

namespace Bridge.XhsSDK
{
    using UnityEngine;
    using System.Text;
    using System.IO;
    using Common;
    using UnityEditor;
    using UnityEditor.Callbacks;

    internal static class ManifestProcessor
    {
        [PostProcessBuild(10001)]
        public static void OnPostprocessBuild(BuildTarget target, string projectPath)
        {
            CopyNativeCode(projectPath);
            // Objective-C 文件路径
            var objectiveCFilePath = Path.Combine(projectPath, Common.ManifestProcessor.NATIVE_CODE_DIR, "xhsapi/XhsApiUnityBridge.java");
            // 读取 Objective-C 文件内容
            var objectiveCCode = new StringBuilder(File.ReadAllText(objectiveCFilePath));
            objectiveCCode = objectiveCCode.Replace("**APPID**", ThirdSDKSettings.Instance.XhsAppId);
            // 将修改后的 Objective-C 代码写回文件中
            File.WriteAllText(objectiveCFilePath, objectiveCCode.ToString());
            
            Common.ManifestProcessor.ReplaceBuildDefinedCache[Common.ManifestProcessor.XHS_DEPENDENCIES] = "implementation(name: 'xhssharesdk-1.1.6', ext:'aar')";
        }
        
        private static void CopyNativeCode(string projectPath)
        {
            var sourcePath = ThirdSDKPackageManager.GetUnityPackagePath(ThirdSDKPackageManager.XhsApiPackageName);
            if (string.IsNullOrEmpty(sourcePath))
            {
                // 这个不是通过ump下载的包，查找工程内部文件夹
                sourcePath = "Assets/ThirdSDK/XhsApi";
            }

            sourcePath += "/Plugins/Android";
            Debug.Log("remotePackagePath===" + sourcePath);
            string targetPath = Path.Combine(projectPath, Common.ManifestProcessor.NATIVE_CODE_DIR, "xhsapi");
            Debug.Log("targetPath===" + targetPath);
            FileTool.DirectoryCopy(sourcePath + "/xhsapi", targetPath);
            FileTool.DirectoryCopy(sourcePath + "/libs", Path.Combine(projectPath, Common.ManifestProcessor.LIB_Dir));
        }
    }
}

#endif