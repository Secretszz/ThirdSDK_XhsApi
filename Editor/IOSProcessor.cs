// *******************************************
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
	using Editor;
	using System.Text;

	/// <summary>
	/// 
	/// </summary>
	internal static class IOSProcessor
	{
		private const string ApiPath = "Libraries/ThirdSDK/XhsApi/Plugins/iOS/XhsApiManager.mm";

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
