
// *******************************************
// Company Name:	深圳市晴天互娱科技有限公司
//
// File Name:		IShareListener.cs
//
// Author Name:		Bridge
//
// Create Time:		2024/02/03 16:56:02
// *******************************************

namespace Bridge.XhsSDK
{
	/// <summary>
	/// 
	/// </summary>
	public interface IShareListener
	{
		void OnSuccess(string sessionId);

		void OnError(string sessionId, int errCode, string errMsg);
	}
}