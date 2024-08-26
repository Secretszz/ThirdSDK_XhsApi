// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		XhsApi.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 19:23:54
// *******************************************

namespace XhsSDK.Api
{
	using Bridge;
	using Listener;
	
	/// <summary>
	/// 
	/// </summary>
	public static class XhsApi
	{
		private static IBridge _bridge;

		/// <summary>
		/// SDK桥接文件
		/// </summary>
		private static IBridge bridgeImpl
		{
			get
			{
				if (_bridge == null)
				{
#if UNITY_IOS && !UNITY_EDITOR
					_bridge = new iOSBridgeImpl();
#elif UNITY_ANDROID && !UNITY_EDITOR
					_bridge = new AndroidBridgeImpl();
#else
					_bridge = new EditorBridge();
#endif
				}

				return _bridge;
			}
		}
		
#if UNITY_IOS
		public const string appKey = "0a42db3502d007df72885eee11f9c596";
		private const string universalLink = "https://sunnygame666.com/zhandoushaonv/";
#else
		private const string appKey = "22b773b290903791777f3b42a0cfbf5a";
		private const string universalLink = "";
#endif

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="listener"></param>
		public static void InitSDK(IInitListener listener)
		{
			bridgeImpl.InitSDK(appKey, universalLink, listener);
		}

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imagePaths">分享的图片列表</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		public static string ShareImage(string title, string content, string[] imagePaths, IShareListener listener)
		{
			return bridgeImpl.ShareImage(title, content, imagePaths, listener);
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
		public static string ShareVideo(string title, string content, string videoPaths, string imagePath, IShareListener listener)
		{
			return bridgeImpl.ShareVideo(title, content, videoPaths, imagePath, listener);
		}

#if UNITY_IOS

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imageData">图片数据</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		public static string ShareImage(string title, string content, byte[] imageData, IShareListener listener)
		{
			return bridgeImpl.ShareImage(title, content, imageData, listener);
		}

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="imageData">图片数据</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		public static string ShareVideo(string title, string content, string videoPaths, byte[] imageData, IShareListener listener)
		{
			return bridgeImpl.ShareVideo(title, content, videoPaths, imageData, listener);
		}

#endif

		/// <summary>
		/// 在小红书打开活动页
		/// </summary>
		/// <param name="url">网页链接，仅支持 http 和 https 链接</param>
		public static void OpenUrlInXhs(string url)
		{
			bridgeImpl.OpenUrlInXhs(url);
		}

		/// <summary>
		/// 小红书是否已经安装
		/// </summary>
		/// <returns>是否已经安装</returns>
		public static bool IsInstalled()
		{
			return bridgeImpl.IsInstalled();
		}
	}
}