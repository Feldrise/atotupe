using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Interfaces;
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
        }

        private void OnAddWallet(object sender, EventArgs args)
        {
            var popup = new EntryPopup("Name of wallet", string.Empty, "OK", "Cancel");
            popup.PopupClosed += (o, closedArgs) => {
                if (closedArgs.Button == "OK")
                {
                    if (string.IsNullOrWhiteSpace(closedArgs.Text)) 
                        return;

                    Wallet wallet = new Wallet {Name = closedArgs.Text};
                    _wallets.Add(wallet);
                }
            };

            popup.Show("Text");
        }

        private void SaveWallets()
        {
            string walletJson = JsonConvert.SerializeObject(_wallets);
            _walletsSaver.SaveWalletAsync(walletJson).GetAwaiter().GetResult();
        }
    }
}
