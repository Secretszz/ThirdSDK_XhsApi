// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		iOSBridgeImpl.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 17:34:49
// *******************************************

#if UNITY_IOS
namespace Bridge.XhsSDK
{
	using Common;
	using Newtonsoft.Json;
	using System.Runtime.InteropServices;
	using AOT;
	using System;
	using UnityEngine;

	/// <summary>
	/// 
	/// </summary>
	internal class iOSBridgeImpl : IBridge
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="listener"></param>
		void IBridge.InitSDK(IBridgeListener listener)
		{
			xhs_initSDK();
			listener?.OnSuccess("");
		}

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imagePaths">分享的图片列表</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string IBridge.ShareImage(string title, string content, string[] imagePaths, IBridgeListener listener)
		{
			ShareCallback._shareListener = listener;
			xhs_shareImage(title, content, JsonConvert.SerializeObject(imagePaths), ShareCallback.OnSuccess, ShareCallback.OnCancel, ShareCallback.OnError);
			return null;
		}

		/// <summary>
		/// 发起图文分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imageData">图片数据</param>
		/// <param name="listener">分享回调</param>
		/// <returns>本次分享的唯一标识，每次分享都会改变</returns>
		string IBridge.ShareImage(string title, string content, byte[] imageData, IBridgeListener listener)
		{
			try
			{
				ShareCallback._shareListener = listener;
				int length = imageData.Length;
				IntPtr buffer = Marshal.AllocHGlobal(length);
				Marshal.Copy(imageData, 0, buffer, length);
				xhs_shareImageByData(title, content, buffer, length, ShareCallback.OnSuccess, ShareCallback.OnCancel, ShareCallback.OnError);
			}
			catch (Exception e)
			{
				Debug.LogError("字节流转指针解析错误：" + e.Message);
				listener?.OnError(-1, e.Message);
			}
			return null;
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
		string IBridge.ShareVideo(string title, string content, string videoPaths, string imagePath, IBridgeListener listener)
		{
			ShareCallback._shareListener = listener;
			xhs_shareVideo(title, content, videoPaths, imagePath, ShareCallback.OnSuccess, ShareCallback.OnCancel, ShareCallback.OnError);
			return null;
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
		string IBridge.ShareVideo(string title, string content, string videoPaths, byte[] imageData, IBridgeListener listener)
		{
			try
			{
				ShareCallback._shareListener = listener;
				int length = imageData.Length;
				IntPtr buffer = Marshal.AllocHGlobal(length);
				Marshal.Copy(imageData, 0, buffer, length);
				xhs_shareVideoByData(title, content, videoPaths, buffer, length, ShareCallback.OnSuccess, ShareCallback.OnCancel, ShareCallback.OnError);
			}
			catch (Exception e)
			{
				Debug.LogError("字节流转指针解析错误：" + e.Message);
				listener?.OnError(-1, e.Message);
			}
			return null;
		}

		/// <summary>
		/// 在小红书打开活动页
		/// </summary>
		/// <param name="url">网页链接，仅支持 http 和 https 链接</param>
		void IBridge.OpenUrlInXhs(string url)
		{
			xhs_openUrlInXhs(url, OpenUrlCallback);
		}

		/// <summary>
		/// 小红书是否已经安装
		/// </summary>
		/// <returns>是否已经安装</returns>
		bool IBridge.IsInstalled()
		{
			return xhs_isInstalled();
		}

		private delegate void U3DBridgeCallback_OpenUrl(bool success, string errMsg);

		private static class ShareCallback
		{
			/// <summary>
			/// 分享回调监听
			/// </summary>
			public static IBridgeListener _shareListener;

			/// <summary>
			/// 支付成功回调桥接函数
			/// </summary>
			/// <param name="result"></param>
			[MonoPInvokeCallback(typeof(U3DBridgeCallback_Success))]
			public static void OnSuccess(string result)
			{
				_shareListener?.OnSuccess(result);
			}

			/// <summary>
			/// 支付用户取消回调桥接函数
			/// </summary>
			[MonoPInvokeCallback(typeof(U3DBridgeCallback_Cancel))]
			public static void OnCancel()
			{
				_shareListener?.OnCancel();
			}

			/// <summary>
			/// 支付错误回调桥接函数
			/// </summary>
			/// <param name="errCode"></param>
			/// <param name="errMsg"></param>
			[MonoPInvokeCallback(typeof(U3DBridgeCallback_Error))]
			public static void OnError(int errCode, string errMsg)
			{
				_shareListener?.OnError(errCode, errMsg);
			}
		}

		[MonoPInvokeCallback(typeof(U3DBridgeCallback_OpenUrl))]
		private static void OpenUrlCallback(bool success, string errMsg)
		{

		}

		/// <summary>
		/// 初始化
		/// </summary>
		[DllImport("__Internal")]
		private static extern void xhs_initSDK();

		/// <summary>
		/// 分享图片
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="imagePaths">分享的图片列表JSON</param>
		/// <param name="onSuccess">成功回调</param>
		/// <param name="onCancel">取消回调</param>
		/// <param name="onError">错误回调</param>
		[DllImport("__Internal")]
		private static extern void xhs_shareImage(string title, string content, string imagePaths, U3DBridgeCallback_Success onSuccess, U3DBridgeCallback_Cancel onCancel, U3DBridgeCallback_Error onError);

		/// <summary>
		/// 分享图片
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="data">图片数据</param>
		/// <param name="length">图片数据长度</param>
		/// <param name="onSuccess">成功回调</param>
		/// <param name="onCancel">取消回调</param>
		/// <param name="onError">错误回调</param>
		[DllImport("__Internal")]
		private static extern void xhs_shareImageByData(string title, string content, IntPtr data, int length, U3DBridgeCallback_Success onSuccess, U3DBridgeCallback_Cancel onCancel, U3DBridgeCallback_Error onError);

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="imagePath">分享封面图片</param>
		/// <param name="onSuccess">成功回调</param>
		/// <param name="onCancel">取消回调</param>
		/// <param name="onError">错误回调</param>
		[DllImport("__Internal")]
		private static extern void xhs_shareVideo(string title, string content, string videoPaths, string imagePath, U3DBridgeCallback_Success onSuccess, U3DBridgeCallback_Cancel onCancel, U3DBridgeCallback_Error onError);

		/// <summary>
		/// 发起视频分享
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="content">文本内容</param>
		/// <param name="videoPaths">分享视频</param>
		/// <param name="datas">图片数据</param>
		/// <param name="length">图片数据长度</param>
		/// <param name="onSuccess">成功回调</param>
		/// <param name="onCancel">取消回调</param>
		/// <param name="onError">错误回调</param>
		[DllImport("__Internal")]
		private static extern void xhs_shareVideoByData(string title, string content, string videoPaths, IntPtr datas, int length, U3DBridgeCallback_Success onSuccess, U3DBridgeCallback_Cancel onCancel, U3DBridgeCallback_Error onError);

		/// <summary>
		/// 在小红书打开活动页
		/// </summary>
		/// <param name="url">网页链接，仅支持 http 和 https 链接</param>
		/// <param name="callback"></param>
		[DllImport("__Internal")]
		private static extern void xhs_openUrlInXhs(string url, U3DBridgeCallback_OpenUrl callback);

		/// <summary>
		/// 小红书是否已经安装
		/// </summary>
		/// <returns>是否已经安装</returns>
		[DllImport("__Internal")]
		private static extern bool xhs_isInstalled();
	}
}
#endif