package com.bridge.xhsapi;

/**
 * 分享监听
 */
public interface IShareListener {
    /**
     * 分享成功
     * @param sessionId 为本次分享的唯一标识，每次分享都会改变
     */
    void onSuccess(String sessionId);

    /**
     * 分享失败
     * @param sessionId 为本次分享的唯一标识，每次分享都会改变
     * @param errCode 错误码
     * @param errMsg 错误信息
     */
    void onError(String sessionId, int errCode, String errMsg);
}
