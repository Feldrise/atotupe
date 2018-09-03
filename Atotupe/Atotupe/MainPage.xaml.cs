using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Interfaces;
using Atotupe.Resources;
using Atotupe.Tools;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Atotupe
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Wallet> _wallets = new ObservableCollection<Wallet>();
        private IWalletsSaver _walletsSaver = DependencyService.Get<IWalletsSaver>();

        public MainPage()
        {
            InitializeComponent();

             _wallets = _walletsSaver.LoadWalletsAsync().GetAwaiter().GetResult();
            Wallets.Wallets = _wallets;

            // Register events
            AddWalletButton.Clicked += OnAddWallet;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SaveWallets();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            double value = 0;

            foreach (var wallet in _wallets)
            {
                value += wallet.Value;
            }

            if (App.CurrenciesMode == "eur")
                ValueLabel.Text = $"{value:0.0000}" + "€";
            else
                ValueLabel.Text = "$" + $"{value:0.0000}";

            WalletNumberLabel.Text = ApplicationText.WalletNumber + _wallets.Count;
        }

        private void OnAddWallet(object sender, EventArgs args)
        {
            var popup = new EntryPopup(ApplicationText.NameOfWallet, string.Empty, ApplicationText.Ok, ApplicationText.Cancel);
            popup.PopupClosed += (o, closedArgs) => {
                if (closedArgs.Button == ApplicationText.Ok)
                {
                    if (string.IsNullOrWhiteSpace(closedArgs.Text)) 
                        return;

                    Wallet wallet = new Wallet {Name = closedArgs.Text};
                    _wallets.Add(wallet);

                    UpdateSummary();
                }
            };

            popup.Show("Text");
        }

        private async void SaveWallets()
        {
            string walletJson = JsonConvert.SerializeObject(_wallets);
            await _walletsSaver.SaveWalletAsync(walletJson);
        }
    }
}
