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
namespace XhsSDK.Bridge
{
	using Listener;
	using UnityEngine;

	/// <summary>
	/// 
	/// </summary>
	internal class EditorBridge : IBridge
	{
		void IBridge.InitSDK(string appkey, string universalLink, IInitListener listener)
		{
			listener?.OnSuccess();
		}

		string IBridge.ShareImage(string title, string content, string[] imagePaths, IShareListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

		string IBridge.ShareVideo(string title, string content, string videoPaths, string imagePath, IShareListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

#if UNITY_IOS

		string IBridge.ShareImage(string title, string content, byte[] imageData, IShareListener listener)
		{
			listener?.OnSuccess("");
			return null;
		}

		string IBridge.ShareVideo(string title, string content, string videoPaths, byte[] imageData, IShareListener listener)
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