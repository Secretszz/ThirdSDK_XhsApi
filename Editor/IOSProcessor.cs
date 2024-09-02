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
	using System.Linq;

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
				var plistPath = Path.Combine(pathToBuildProject, "Info.plist");
				var plist = new PlistDocument();
				plist.ReadFromFile(plistPath);
				var rootDic = plist.root;
			
				var items = new[]
				{
						"xhsdiscover",
				};
			
				PlistElementArray plistElementList = rootDic.GetElementArray("LSApplicationQueriesSchemes");
				string[] list = plistElementList.values.Select(x => x.AsString()).ToArray();
				foreach (var t in items)
				{
					if (!list.Contains(t))
					{
						plistElementList.AddString(t);
					}
				}
			
				var array = rootDic.GetElementArray("CFBundleURLTypes");
				PlistElementDict wxURLType = array.AddDict();
				wxURLType.SetString("CFBundleTypeRole", "Editor");
				wxURLType.SetString("CFBundleURLName", "xiaohongshu");
				wxURLType.CreateArray("CFBundleURLSchemes").AddString($"xhs{XhsApi.appKey}");
				plist.WriteToFile(plistPath);
			}
		}

		private static PlistElementArray GetElementArray(this PlistElementDict rootDict, string key)
		{
			return rootDict.values.TryGetValue(key, out var element) ? element.AsArray() : rootDict.CreateArray(key);
		}
	}
}
#endif
