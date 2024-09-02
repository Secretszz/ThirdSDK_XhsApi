// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		AndroidBridgeImpl.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 16:16:05
// *******************************************

#if UNITY_ANDROID
namespace Bridge.XhsSDK
{
	using Listener;
	using UnityEngine;

	/// <summary>
	/// 
	/// </summary>
	internal class AndroidBridgeImpl : IBridge
	{
		private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
		private const string ManagerClassName = "com.bridge.xhsapi.XhsApiUnityBridge";
		private const string RegisterClassName = "com.bridge.xhsapi.IRegisterListener";
		private const string ShareClassName = "com.bridge.xhsapi.IShareListener";

		private static AndroidJavaClass bridge;
		private static AndroidJavaObject currentActivity;

		void IBridge.InitSDK(string appKey, string universalLink, IInitListener listener)
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass(UnityPlayerClassName);
			currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			bridge = new AndroidJavaClass(ManagerClassName);
			bridge.CallStatic("registerApp", currentActivity, appKey, new RegisterCallback(listener));
		}

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imagePaths">分享的图片列表</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string IBridge.ShareImage(string title, string content, string[] imagePaths, IShareListener listener)
		{
			return bridge.CallStatic<string>("shareImage", currentActivity, title, content, imagePaths, new ShareCallback(listener));
		}

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="imagePath">分享封面图片</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string IBridge.ShareVideo(string title, string content, string videoPaths, string imagePath, IShareListener listener)
		{
			return bridge.CallStatic<string>("shareVideo", currentActivity, title, content, videoPaths, imagePath, new ShareCallback(listener));
		}

		/// <summary>
		/// 在小红书打开活动页
		/// </summary>
		/// <param name="url">网页链接，仅支持 http 和 https 链接</param>
		void IBridge.OpenUrlInXhs(string url)
		{
			bridge.CallStatic("openUrlInXhs", currentActivity, url);
		}

		/// <summary>
		/// 小红书是否已经安装
		/// </summary>
		/// <returns>是否已经安装</returns>
		bool IBridge.IsInstalled()
		{
			return bridge.CallStatic<bool>("isXhsInstalled", currentActivity);
		}

		/// <summary>
		/// 初始化回调
		/// </summary>
		private class RegisterCallback : AndroidJavaProxy
		{
			public RegisterCallback(IInitListener listener) : base(RegisterClassName)
			{
				this.listener = listener;
			}

			private IInitListener listener;

			public void onSuccess()
			{
				listener?.OnSuccess();
			}

			public void onError(int errorCode, string errorMessage)
			{
				listener?.OnError(errorCode, errorMessage);
			}
		}

		/// <summary>
		/// 分享回调
		/// </summary>
		private class ShareCallback : AndroidJavaProxy
		{
			public ShareCallback(IShareListener listener) : base(ShareClassName)
			{
				this.listener = listener;
			}

			private IShareListener listener;

			/// <summary>
			/// 分享成功回调
			/// </summary>
			/// <param name="sessionId">本次分享成功的对话id</param>
			public void onSuccess(string sessionId)
			{
				listener?.OnSuccess(sessionId);
			}

			/// <summary>
			/// 分享失败回调
			/// </summary>
			/// <param name="sessionId">本次分享成功的对话id</param>
			/// <param name="errorCode">错误码</param>
			/// <param name="errorMessage">错误信息</param>
			public void onError(string sessionId, int errorCode, string errorMessage)
			{
				listener?.OnError(sessionId, errorCode, errorMessage);
			}
		}
	}
}
#endif