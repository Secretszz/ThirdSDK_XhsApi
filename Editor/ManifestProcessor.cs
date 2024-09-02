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
    using System.Text;
    using System.IO;
    using Editor;
    using UnityEditor;
    using UnityEditor.Callbacks;

    internal static class ManifestProcessor
    {
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string projectPath)
        {
            ThirdSDKSettings settings = ThirdSDKSettings.LoadInstance();
            // Objective-C 文件路径
            var objectiveCFilePath = $"{projectPath}/unityLibrary/src/main/java/com/bridge/xhsapi/XhsApiUnityBridge.java";
            // 读取 Objective-C 文件内容
            var objectiveCCode = new StringBuilder(File.ReadAllText(objectiveCFilePath));
            objectiveCCode = objectiveCCode.Replace("**APPID**", settings.XhsAppId);
            // 将修改后的 Objective-C 代码写回文件中
            File.WriteAllText(objectiveCFilePath, objectiveCCode.ToString());
        }
    }
}

#endif