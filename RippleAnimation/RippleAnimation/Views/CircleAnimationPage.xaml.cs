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
    public partial class CircleAnimationPage : ContentPage
    {
        public CircleAnimationPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            _ = MainThread.InvokeOnMainThreadAsync(async () => await ExecuteRectangleAnimationAndNavigateBack());
        }

        private async Task ExecuteRectangleAnimationAndNavigateBack()
        {
            new Animation
            {
                { 0, 0.01, new Animation(v => RectangleIn.IsVisible = true) },
                { 0.01, 0.5, new Animation(v => RectangleIn.ScaleTo(15)) },
                { 0.5, 0.6, new Animation(v => BtnBack.FadeTo(0, 100)) },
                { 0.5, 0.51, new Animation(v => RectangleIn.FadeTo(0, 100)) }
            }.Commit(this, "rectangleAnimation", 60, 1500, Easing.Linear);

            await Task.Delay(1500);

            Navigation.InsertPageBefore(new MainPage(), this);
            _ = await Navigation.PopAsync(false);
        }
    }
}