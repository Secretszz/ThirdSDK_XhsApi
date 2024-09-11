package com.bridge.xhsapi;

import android.app.Activity;
import android.util.Log;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.bridge.common.listener.IBridgeListener;
import com.xingin.xhssharesdk.XhsShareSdkTools;
import com.xingin.xhssharesdk.callback.XhsShareCallback;
import com.xingin.xhssharesdk.callback.XhsShareRegisterCallback;
import com.xingin.xhssharesdk.core.XhsShareSdk;
import com.xingin.xhssharesdk.model.config.XhsShareGlobalConfig;
import com.xingin.xhssharesdk.model.sharedata.XhsImageInfo;
import com.xingin.xhssharesdk.model.sharedata.XhsImageResourceBean;
import com.xingin.xhssharesdk.model.sharedata.XhsNote;
import com.xingin.xhssharesdk.model.sharedata.XhsVideoInfo;
import com.xingin.xhssharesdk.model.sharedata.XhsVideoResourceBean;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

/**
 * 小红书桥接
 */
public class XhsApiUnityBridge {
    private static final String TAG = XhsApiUnityBridge.class.getName();

    /**
     * 注册 app （即初始化）
     * @param activity 主Activity
     * @param listener 初始化回调
     */
    public static void registerApp(Activity activity, IBridgeListener listener){
        XhsShareGlobalConfig config = new XhsShareGlobalConfig()
                .setEnableLog(true);
        XhsShareSdk.registerApp(activity, "", config, new RegisterCallback(listener));
    }

    /**
     * 发起图文分享
     * @param activity Activity
     * @param title 标题
     * @param content 文本内容
     * @param imagePaths 分享的图片列表
     * @param listener 分享回调
     * @return 本次分享的唯一标识，每次分享都会改变
     */
    @NonNull
    public static String shareImage(Activity activity, String title, String content, @NonNull String[] imagePaths, IBridgeListener listener){
        List<XhsImageResourceBean> list = new ArrayList<>();
        for (String path : imagePaths){
            list.add(new XhsImageResourceBean(new File(path)));
        }
        XhsNote note = new XhsNote()
                .setTitle(title)
                .setContent(content)
                .setImageInfo(new XhsImageInfo(list));

        XhsShareSdk.setShareCallback(new ShareCallback(listener));
        return XhsShareSdk.shareNote(activity, note);
    }

    /**
     * 发起视频分享
     * @param activity Activity
     * @param title 标题
     * @param content 文本内容
     * @param videoPath 分享视频
     * @param imagePath 分享封面图片
     * @param listener 分享回调
     * @return 本次分享的唯一标识，每次分享都会改变
     */
    @NonNull
    public static String shareVideo(Activity activity, String title, String content, @NonNull String videoPath, @NonNull String imagePath, IBridgeListener listener){
        XhsNote note = new XhsNote()
                .setTitle(title)
                .setContent(content)
                .setVideoInfo(new XhsVideoInfo(new XhsVideoResourceBean(new File(videoPath)), new XhsImageResourceBean(new File(imagePath))));

        XhsShareSdk.setShareCallback(new ShareCallback(listener));
        return XhsShareSdk.shareNote(activity, note);
    }

    /**
     * 在小红书打开活动页
     * @param activity - Activity
     * @param url - 网页链接，仅支持 http 和 https 链接
     */
    public static void openUrlInXhs(Activity activity, String url){
        XhsShareSdk.openUrlInXhs(activity, url);
    }

    /**
     * 小红书是否已经安装
     * @return 是否已经安装
     */
    public static boolean isXhsInstalled(Activity activity){
        return XhsShareSdkTools.isXhsInstalled(activity);
    }

    /**
     * 禁止外部创建实例
     */
    private XhsApiUnityBridge(){}

    /**
     * 注册App回调
     */
    private static class RegisterCallback implements XhsShareRegisterCallback {

        /**
         * 注册App监听
         */
        private final IBridgeListener listener;

        /**
         * 实例化注册App回调
         * @param listener 回调内的Unity监听
         */
        public RegisterCallback(IBridgeListener listener){
            this.listener = listener;
        }

        /**
         * 注册成功回调
         */
        @Override
        public void onSuccess() {
            Log.i(TAG, "RegisterCallback.onSuccess: register success");
            listener.onSuccess("success");
        }

        /**
         * 注册错误回调
         * @param errCode 错误码
         * @param errMsg 错误信息
         * @param e 代码执行错误时会返回
         */
        @Override
        public void onError(int errCode, String errMsg, @Nullable Exception e) {
            Log.e(TAG, "RegisterCallback.onError: ", e);
            listener.onError(errCode, errMsg);
        }
    }

    /**
     * 分享回调
     */
    private static class ShareCallback implements XhsShareCallback{

        /**
         * 分享监听
         */
        private final IBridgeListener listener;

        /**
         * 实例化分享回调
         * @param listener 回调内的Unity监听
         */
        private ShareCallback(IBridgeListener listener) {
            this.listener = listener;
        }

        /**
         * 分享成功
         * @param sessionId 为本次分享的唯一标识，每次分享都会改变
         */
        @Override
        public void onSuccess(String sessionId) {
            Log.i(TAG, "ShareCallback.onSuccess: " + sessionId);
            listener.onSuccess(sessionId);
        }

        /**
         * 分享失败
         * @param sessionId 为本次分享的唯一标识，每次分享都会改变
         * @param errCode 错误码
         * @param errMsg 错误信息
         * @param throwable 代码执行错误时会返回
         */
        @Override
        public void onError(@NonNull String sessionId, int errCode, @NonNull String errMsg, @Nullable Throwable throwable) {
            Log.e(TAG, "ShareCallback.onError: ", throwable);
            listener.onError(errCode, errMsg);
        }
        
        /**
         * 分享失败
         * @param sessionId 为本次分享的唯一标识，每次分享都会改变
         * @param errCode 错误码 - 旧版
         * @param newErrCode 错误码 - 新版
         * @param errMsg 错误信息
         * @param throwable 代码执行错误时会返回
         */
        @Override
        public void onError2(@NonNull String sessionId, int errCode, int newErrCode, @NonNull String errMsg, @Nullable Throwable throwable) {
            Log.e(TAG, "ShareCallback.onError: ===", throwable);
            listener.onError(newErrCode, errMsg);
        }
    }
}
