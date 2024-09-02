package com.bridge.xhsapi;

/**
 * 注册App监听
 */
public interface IRegisterListener {
    /**
     * 注册App成功
     */
    void onSuccess();

    /**
     * 注册App失败
     * @param errCode 错误码
     * @param errMsg 错误信息
     */
    void onError(int errCode, String errMsg);
}
