//
//  XhsApiManager.h
//  UnityFramework
//
//  Created by 晴天 on 2023/10/19.
//

#import <XiaoHongShuOpenSDK/XHSApi.h>

/**
 绑定第三方账号成功事件
 */
typedef void(* U3DBridgeCallback_Share)(int, const char *);
typedef void(* U3DBridgeCallback_OpenUrl)(bool, const char *);

@interface XhsApiManager : NSObject<XHSApiDelegate>

@property (nonatomic, assign) U3DBridgeCallback_Share shareCallback;
@property (nonatomic, assign) U3DBridgeCallback_OpenUrl openUrlCallback;

+ (XhsApiManager *) instance;

- (BOOL)application:(UIApplication *)application
            openURL:(nonnull NSURL *)url
            options:(nonnull NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;

- (BOOL)application:(UIApplication *)application
continueUserActivity:(NSUserActivity *)userActivity
 restorationHandler:(void(^)(NSArray<id<UIUserActivityRestoring>> * __nullable restorableObjects))restorationHandler;

@end
