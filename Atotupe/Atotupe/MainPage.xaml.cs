using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Tools;
using Xamarin.Forms;

namespace Atotupe
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Register events
            AddWalletButton.Clicked += OnAddWallet;
        }

        private void OnAddWallet(object sender, EventArgs args)
        {
            var popup = new EntryPopup("Name of wallet", string.Empty, "OK", "Cancel");
            popup.PopupClosed += (o, closedArgs) => {
                if (closedArgs.Button == "OK")
                {
                    Wallet wallet = new Wallet {Name = closedArgs.Text};
                    Wallets.AddWallet(wallet);
                }
            };

            popup.Show("Text");
        }
    }
}
