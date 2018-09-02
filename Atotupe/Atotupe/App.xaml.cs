using System;
using Atotupe.Pages;
using Atotupe.Tools;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Atotupe
{
    public partial class App : Application
    {
        public static EventHandler<CurrencyPriceUpdatedArgs> CurrencyPriceUpdated;

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

        public static double GetPriceOfCurrency(string code)
        {
            switch (code)
            {
                case "BTC":
                    return UsdBtc;
                case "BCH":
                    return UsdBch;
                case "ETH":
                    return UsdEth;
                case "LTC":
                    return UsdLtc;
                case "XRP":
                    return UsdXrp;
                default:
                    return 0;
            }
        }

        public static void UpdatePrices()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                BitstampCaller bitstamp = new BitstampCaller();
                bitstamp.GetAllUsd();
            }
        }

        public static double UsdBtc = 0;
        public static double UsdBch = 0;
        public static double UsdEth = 0;
        public static double UsdLtc = 0;
        public static double UsdXrp = 0;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        //void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        //{
        //    //Type currentPage = this.MainPage.GetType();
        //    //if (e.IsConnected && currentPage != typeof(NetworkViewPage))
        //    //    this.MainPage = new NetworkViewPage();
        //    //else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
        //    //    this.MainPage = new NoNetworkPage();
        //}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }

    public class CurrencyPriceUpdatedArgs : EventArgs
    {
        public string CurrencyCode { get; set; }
        public double NewPrice { get; set; }
    }
}
