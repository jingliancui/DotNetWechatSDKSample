using Foundation;
using UIKit;
using WXApi_Api;

namespace SampleApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    private Platforms.iOS.CustomizeIOSWXApiDelegate wxApiDelegate;

    protected override MauiApp CreateMauiApp()
    {
        wxApiDelegate = new Platforms.iOS.CustomizeIOSWXApiDelegate();
        return MauiProgram.CreateMauiApp();
    }

    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
        return WXApi.HandleOpenURL(url, wxApiDelegate);
    }

    public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
    {
        return WXApi.HandleOpenUniversalLink(userActivity, wxApiDelegate);
    }

}

