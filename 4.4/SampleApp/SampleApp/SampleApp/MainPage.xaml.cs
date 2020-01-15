using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<object, bool>(this, PubSubMessage.Registered, (sender, args) => 
            {
                RegisterResultLabel.Text = args.ToString();
            });
        }

        private void RegisterBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), PubSubMessage.Register);
        }

        private void ShareToFriendBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), PubSubMessage.ShareToFriend);
        }

        private void ShareToFavouriteBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), PubSubMessage.ShareToFavourite);
        }

        private void ShareToTimelineBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), PubSubMessage.ShareToTimeline);
        }
    }
}
