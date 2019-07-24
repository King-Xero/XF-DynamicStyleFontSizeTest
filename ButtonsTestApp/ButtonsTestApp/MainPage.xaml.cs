using System;
using System.Windows.Input;
using ButtonsTestApp.Resources.FontScaling;
using Xamarin.Forms;

namespace ButtonsTestApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IControlFontScale _fontScaler;

        public MainPage()
        {
            InitializeComponent();
            _fontScaler = new FontScaleController();
        }

        private void IncreaseFont_OnClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>_fontScaler.IncreaseFontScaling());
        }

        private void DecreaseFont_OnClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => _fontScaler.DecreaseFontScaling());
        }
    }
}
