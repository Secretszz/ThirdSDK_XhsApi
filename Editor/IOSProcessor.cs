﻿// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		IOSProcessor.cs
//
// Author Name:		Bridge
//
// Create Time:		2023/12/26 18:23:11
// *******************************************

#if UNITY_IOS
namespace Bridge.XhsSDK
{
	using System.IO;
	using UnityEditor;
	using UnityEditor.Callbacks;
	using UnityEditor.iOS.Xcode;
	using Common;
	using System.Text;

	/// <summary>
	/// 
	/// </summary>
	internal static class IOSProcessor
	{
		[PostProcessBuild(10002)]
		public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
		{
			if (target == BuildTarget.iOS)
			{
				ThirdSDKSettings instance = ThirdSDKSettings.Instance;
				var plistPath = Path.Combine(pathToBuildProject, "Info.plist");
				var plist = new PlistDocument();
				plist.ReadFromFile(plistPath);
				var rootDic = plist.root;

				var items = new[]
				{
						"xhsdiscover",
				};

				rootDic.AddApplicationQueriesSchemes(items);

				var array = rootDic.GetElementArray("CFBundleURLTypes");
				array.AddCFBundleURLTypes("Editor", "xiaohongshu", new[] { $"xhs{instance.XhsAppId_iOS}" });
				plist.WriteToFile(plistPath);

				var sourcePath = ThirdSDKPackageManager.GetUnityPackagePath(ThirdSDKPackageManager.WxApiPackageName);
				string ApiPath;
				if (string.IsNullOrEmpty(sourcePath))
				{
					ApiPath = "Libraries/ThirdSDK/XhsApi/Plugins/iOS/XhsApiManager.mm";
				}
				else
				{
					ApiPath = "Libraries/com.bridge.xhsapi/Plugins/iOS/XhsApiManager.mm";
				}
				var objectiveCFilePath = Path.Combine(pathToBuildProject, ApiPath);
				StringBuilder objectiveCCode = new StringBuilder(File.ReadAllText(objectiveCFilePath));
				objectiveCCode.Replace("**APPID**", instance.XhsAppId_iOS);
				objectiveCCode.Replace("**UNILINK**", instance.UniversalLink);
				// 将修改后的 Objective-C 代码写回文件中
				File.WriteAllText(objectiveCFilePath, objectiveCCode.ToString());
			}
		}
	}
}
#endif
