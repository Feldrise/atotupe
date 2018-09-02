using System;
using Atotupe.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Atotupe
{
    public partial class App : Application
    {
        public static string GetCodeOfCurrency(string name)
        {
            switch (name)
            {
                case "Bitcoin":
                    return "BTC";
                case "Bitcoin Cash":
                    return "BCH";
                case "Ethereum":
                    return "ETH";
                case "Litecoin":
                    return "LTC";
                case "Ripple":
                    return "XRP";
                default:
                    return "UNK";
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
