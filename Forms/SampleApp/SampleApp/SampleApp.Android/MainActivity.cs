using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Tencent.MM.Opensdk.Openapi;
using Android.Content;
using Com.Tencent.MM.Opensdk.Constants;
using Xamarin.Forms;
using Com.Tencent.MM.Opensdk.Modelmsg;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            MessagingCenter.Subscribe<object>(this, PubSubMessage.Register, d => 
            {
                var result = RegToWx();

                MessagingCenter.Send(new object(), PubSubMessage.Registered, result);
            });
            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToFriend, d => 
            {
                var text = "这是Xamarin发送到微信朋友的消息！";
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
                    Message=msg,
                    Scene= SendMessageToWX.Req.WXSceneSession//分享到对话
                };

                //调用api接口，发送数据到微信
                wxApi.SendReq(req);
            });
            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToFavourite, d =>
            {
                var text = "这是Xamarin发送到微信收藏的消息！";
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
                wxApi.SendReq(req);
            });
            MessagingCenter.Subscribe<object>(this, PubSubMessage.ShareToTimeline, d =>
            {
                var text = "这是Xamarin发送到微信朋友圈的消息！";
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
                wxApi.SendReq(req);
            });
            //var wxBroadcastReceiver = new WxBroadcastReceiver(wxApi, appId);
            //RegisterReceiver(wxBroadcastReceiver, new IntentFilter(ConstantsAPI.ActionRefreshWxapp));

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region 微信相关
        //https://developers.weixin.qq.com/doc/oplatform/Mobile_App/Access_Guide/Android.html
        private readonly string appId = "";
        private IWXAPI wxApi;
        private bool RegToWx() 
        {
            wxApi = WXAPIFactory.CreateWXAPI(this, appId, true);
            return wxApi.RegisterApp(appId);
        }
        #endregion

    }

    public class WxBroadcastReceiver : BroadcastReceiver
    {
        private IWXAPI wxApi;

        private string appId;

        public WxBroadcastReceiver(IWXAPI _wxApi, string _appId)
        {
            wxApi = _wxApi;
            appId = _appId;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var result = wxApi.RegisterApp(appId);
        }
    }
}