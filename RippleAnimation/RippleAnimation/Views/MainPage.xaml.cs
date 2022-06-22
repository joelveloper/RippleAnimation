using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RippleAnimation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            _ = MainThread.InvokeOnMainThreadAsync(async () => await ExecuteCirclesAnimationAndNavigate());
        }

        private async Task ExecuteCirclesAnimationAndNavigate()
        {
            new Animation
            {
                { 0, 0.01, new Animation(v => CircleIn.IsVisible = true) },
                { 0.01, 0.5, new Animation(v => CircleIn.ScaleTo(15)) },
                { 0.5, 0.51, new Animation(v => CircleIn.IsVisible = false) },
                { 0.5, 0.51, new Animation(v => CircleOut.IsVisible = true) },
                { 0.51, 0.6, new Animation(v => ToolBar.FadeTo(0, 150)) },
                { 0.51, 0.6, new Animation(v => content.FadeTo(0, 150)) },
                { 0.51, 0.6, new Animation(v => frame.FadeTo(0, 150)) },
                { 0.51, 1, new Animation(v => CircleOut.ScaleTo(0)) },
                { 0.99, 1,  new Animation(v => CircleOut.IsVisible = false) }
            }.Commit(this, "circleAnimation", 60, 1500, Easing.Linear);

            await Task.Delay(1500);

            await Navigation.PushAsync(new CircleAnimationPage(), false);
        }
    }
}