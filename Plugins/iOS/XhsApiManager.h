//
//  XhsApiManager.h
//  UnityFramework
//
//  Created by 晴天 on 2023/10/19.
//

#import <XiaoHongShuOpenSDK/XHSApi.h>
#import "CommonApi.h"

@interface XhsApiManager : NSObject<XHSApiDelegate>

@property (nonatomic, assign) U3DBridgeCallback_Success onSuccess;
@property (nonatomic, assign) U3DBridgeCallback_Cancel onCancel;
@property (nonatomic, assign) U3DBridgeCallback_Error onError;

+ (XhsApiManager *) instance;

- (BOOL)application:(UIApplication *)application
            openURL:(nonnull NSURL *)url
            options:(nonnull NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;

- (BOOL)application:(UIApplication *)application
continueUserActivity:(NSUserActivity *)userActivity
 restorationHandler:(void(^)(NSArray<id<UIUserActivityRestoring>> * __nullable restorableObjects))restorationHandler;

@end
