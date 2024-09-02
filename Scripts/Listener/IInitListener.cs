
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
	/// <summary>
	/// 初始化监听
	/// </summary>
	public interface IInitListener
	{
		void OnSuccess();

		void OnError(int errCode, string errMsg);
	}
}
