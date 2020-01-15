using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WechatSDK.WXApi;
using WechatSDK.WXApiObject;
using Xamarin.Forms;

namespace SampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            wxApiDelegate = new CustomizeWXApiDelegate();
            MessagingCenter.Subscribe<object>(this, PubSubMessage.Register, d => 
            {
                var result = WXApi.RegisterApp(appid: "", universalLink: "");
                MessagingCenter.Send(new object(), PubSubMessage.Registered, result);
            });

            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToFriend, d => 
            {
                var req = new SendMessageToWXReq()
                {
                    Scene = (int)WXScene.Session,
                    Text="这是Xamarin发送的消息！",
                    BText=true,
                };
                WXApi.SendReq(req, isOK => 
                {

                });
            });

            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToFavourite, d => 
            {
                var req = new SendMessageToWXReq()
                {
                    Scene = (int)WXScene.Favorite,
                    Text = "这是Xamarin发送的消息！",
                    BText = true,
                };
                WXApi.SendReq(req, isOK =>
                {

                });
            });

            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToTimeline,d => 
            {
                var req = new SendMessageToWXReq()
                {
                    Scene = (int)WXScene.Timeline,
                    Text = "这是Xamarin发送的消息！",
                    BText = true,
                };
                WXApi.SendReq(req, isOK =>
                {

                });
            });

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool HandleOpenURL(UIApplication application, NSUrl url)
        {
            //return base.HandleOpenURL(application, url);
            return WXApi.HandleOpenURL(url, wxApiDelegate);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //return base.OpenUrl(application, url, sourceApplication, annotation);
            return WXApi.HandleOpenURL(url, wxApiDelegate);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            //return base.ContinueUserActivity(application, userActivity, completionHandler);
            return WXApi.HandleOpenUniversalLink(userActivity, wxApiDelegate);
        }

        private CustomizeWXApiDelegate wxApiDelegate;
    }
}
