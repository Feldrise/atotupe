using System;
using Atotupe.Pages;
using Atotupe.Tools;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
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
                    return BtcPrice;
                case "BCH":
                    return BchPrice;
                case "ETH":
                    return EthPrice;
                case "LTC":
                    return LtcPrice;
                case "XRP":
                    return XrpPrice;
                default:
                    return 0;
            }
        }

        public static void UpdatePrices()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                BitstampCaller bitstamp = new BitstampCaller();
                bitstamp.GetAll();
            }
        }

        public static string CurrenciesMode = "usd";

        public static double BtcPrice = 0;
        public static double BchPrice = 0;
        public static double EthPrice = 0;
        public static double LtcPrice = 0;
        public static double XrpPrice = 0;

        public App()
        {
            InitializeComponent();

            if (CrossConnectivity.Current.IsConnected)
                UpdatePrices();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected && BtcPrice <= 0)
            {
                UpdatePrices();
            }
            //Type currentPage = this.MainPage.GetType();
            //if (e.IsConnected && currentPage != typeof(NetworkViewPage))
            //    this.MainPage = new NetworkViewPage();
            //else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
            //    this.MainPage = new NoNetworkPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (CrossConnectivity.Current.IsConnected)
                UpdatePrices();
        }

    }

    public class CurrencyPriceUpdatedArgs : EventArgs
    {
        public string CurrencyCode { get; set; }
        public double NewPrice { get; set; }
    }
}
