namespace SampleApp;

#if ANDROID
using Com.Tencent.MM.Opensdk.Modelmsg;
using Com.Tencent.MM.Opensdk.Openapi;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;
#endif

#if IOS
using WXApi_Api;
using WXApiObject_Api;
using WXApiObject_Structs;
#endif

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

#if ANDROID
    private IWXAPI wxAPI;
#endif

    void RegisterBtn_Clicked(System.Object sender, System.EventArgs e)
    {
#if IOS
        var result = WXApi.RegisterApp(appid: "", universalLink: "");
        RegisterResultLabel.Text = result.ToString();
#endif

#if ANDROID
        var appid = "";
        var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        wxAPI = WXAPIFactory.CreateWXAPI(p0: activity, p1: appid, p2: true);
        var result = wxAPI.RegisterApp(appid);
        RegisterResultLabel.Text = result.ToString();
#endif
    }

    void ShareToFriendBtn_Clicked(System.Object sender, System.EventArgs e)
    {
#if IOS
        var req = new SendMessageToWXReq()
        {
            Scene = (int)WXScene.Session,
            Text = "这是MAUI发送的消息！",
            BText = true,
        };
        WXApi.SendReq(req, isOK =>
        {

        });
#endif

#if ANDROID
        var text = "这是MAUI发送到微信朋友的消息！";
        //初始化一个 WXTextObject 对象，填写分享的文本内容
        var textObj = new WXTextObject
        {
            Text = text
        };

        //用 WXTextObject 对象初始化一个 WXMediaMessage 对象
        var msg = new WXMediaMessage
        {
            TheMediaObject = textObj,
            Description = text
        };

        var req = new SendMessageToWX.Req()
        {
            Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
            Message = msg,
            Scene = SendMessageToWX.Req.WXSceneSession//分享到对话
        };

        //调用api接口，发送数据到微信
        wxAPI.SendReq(req);
#endif
    }

    void ShareToFavouriteBtn_Clicked(System.Object sender, System.EventArgs e)
    {
#if IOS
        var req = new SendMessageToWXReq()
        {
            Scene = (int)WXScene.Favorite,
            Text = "这是MAUI发送的消息！",
            BText = true,
        };
        WXApi.SendReq(req, isOK =>
        {

        });
#endif

#if ANDROID
        var text = "这是MAUI发送到微信收藏的消息！";
        //初始化一个 WXTextObject 对象，填写分享的文本内容
        var textObj = new WXTextObject
        {
            Text = text
        };

        //用 WXTextObject 对象初始化一个 WXMediaMessage 对象
        var msg = new WXMediaMessage
        {
            TheMediaObject = textObj,
            Description = text
        };

        var req = new SendMessageToWX.Req()
        {
            Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
            Message = msg,
            Scene = SendMessageToWX.Req.WXSceneFavorite//分享到收藏
        };

        //调用api接口，发送数据到微信
        wxAPI.SendReq(req);
#endif
    }

    void ShareToTimelineBtn_Clicked(System.Object sender, System.EventArgs e)
    {
#if IOS
        var req = new SendMessageToWXReq()
        {
            Scene = (int)WXScene.Timeline,
            Text = "这是MAUI发送的消息！",
            BText = true,
        };
        WXApi.SendReq(req, isOK =>
        {

        });
#endif

#if ANDROID
        var text = "这是MAUI发送到微信朋友圈的消息！";
        //初始化一个 WXTextObject 对象，填写分享的文本内容
        var textObj = new WXTextObject
        {
            Text = text
        };

        //用 WXTextObject 对象初始化一个 WXMediaMessage 对象
        var msg = new WXMediaMessage
        {
            TheMediaObject = textObj,
            Description = text
        };

        var req = new SendMessageToWX.Req()
        {
            Transaction = DateTime.Now.ToFileTimeUtc().ToString(),
            Message = msg,
            Scene = SendMessageToWX.Req.WXSceneTimeline//分享到朋友圈
        };

        //调用api接口，发送数据到微信
        wxAPI.SendReq(req);
#endif
    }
}


