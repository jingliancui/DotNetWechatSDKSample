namespace SampleApp;
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

    void RegisterBtn_Clicked(System.Object sender, System.EventArgs e)
    {
#if IOS
        var result = WXApi.RegisterApp(appid: "", universalLink: "");
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
    }
}


