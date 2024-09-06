// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		IInitListener.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 16:04:32
// *******************************************

namespace Bridge.XhsSDK
{
	using Common;
	
	internal interface IBridge
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="listener"></param>
		void InitSDK(IInitListener listener);

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imagePaths">分享的图片列表</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string ShareImage(string title, string content, string[] imagePaths, IShareListener listener);

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="imagePath">分享封面图片</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string ShareVideo(string title, string content, string videoPaths, string imagePath, IShareListener listener);

#if UNITY_IOS

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imageData">图片数据</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string ShareImage(string title, string content, byte[] imageData, IShareListener listener);

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="imageData">图片数据</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string ShareVideo(string title, string content, string videoPaths, byte[] imageData, IShareListener listener);

#endif

		/// <summary>
		/// 在小红书打开活动页
		/// </summary>
		/// <param name="url">网页链接，仅支持 http 和 https 链接</param>
		void OpenUrlInXhs(string url);

		/// <summary>
		/// 小红书是否已经安装
		/// </summary>
		/// <returns>是否已经安装</returns>
		bool IsInstalled();
	}
}