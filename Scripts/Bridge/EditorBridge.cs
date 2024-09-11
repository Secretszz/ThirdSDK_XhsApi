// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		EditorBridge.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 19:26:41
// *******************************************

#if UNITY_EDITOR
namespace Bridge.XhsSDK
{
	using Common;
	using UnityEngine;

	/// <summary>
	/// 
	/// </summary>
	internal class EditorBridge : IBridge
	{
		void IBridge.InitSDK(IBridgeListener listener)
		{
			listener?.OnSuccess("");
		}

		string IBridge.ShareImage(string title, string content, string[] imagePaths, IBridgeListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

		string IBridge.ShareVideo(string title, string content, string videoPaths, string imagePath, IBridgeListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

#if UNITY_IOS

		string IBridge.ShareImage(string title, string content, byte[] imageData, IBridgeListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

		string IBridge.ShareVideo(string title, string content, string videoPaths, byte[] imageData, IBridgeListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

#endif

		void IBridge.OpenUrlInXhs(string url)
		{
			Application.OpenURL(url);
		}

		bool IBridge.IsInstalled()
		{
			return false;
		}
	}
}
#endif