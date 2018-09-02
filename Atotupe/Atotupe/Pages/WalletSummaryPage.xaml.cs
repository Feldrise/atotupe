using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Tools;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Atotupe.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalletSummaryPage : ContentPage
	{
	    private Wallet _wallet;

	    public Wallet Wallet
	    {
	        get => _wallet;
	        set
	        {
	            _wallet = value;
	            Currencies.Currencies = _wallet.Currencies;

	            BindingContext = _wallet;
	        }
	    }

		public WalletSummaryPage ()
		{
			InitializeComponent ();

            // Register events
		    AddCurrency.Clicked += OnAddCurrency;
            NameLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnNameClicked)
            });
		}

	    private async void OnAddCurrency(object sender, EventArgs args)
	    {
            List<string> missingCurrencies = new List<string>();

            if (!_wallet.ContainsCurrency("BTC"))
                missingCurrencies.Add("Bitcoin");
            if (!_wallet.ContainsCurrency("BCH"))
                missingCurrencies.Add("Bitcoin Cash");
            if (!_wallet.ContainsCurrency("ETH"))
                missingCurrencies.Add("Ethereum");
            if (!_wallet.ContainsCurrency("LTC"))
                missingCurrencies.Add("Litecoin");
            if (!_wallet.ContainsCurrency("XRP"))
                missingCurrencies.Add("Ripple");

	        var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, missingCurrencies.ToArray());

	        if (action != "Cancel" && !string.IsNullOrWhiteSpace(action))
	        {
	            var item = new Currency
	            {
	                Name = action,
	                Code = App.GetCodeOfCurrency(action),
	                Number = 0,
	                Price = App.GetPriceOfCurrency(App.GetCodeOfCurrency(action)),
	                Value = 0
	            };

	            if (!_wallet.ContainsCurrency(item))
	            {
                    _wallet.AddCurrency(item);
	            }
	        }
        }

	    private void OnNameClicked()
	    {
	        var popup = new EntryPopup("Name", _wallet.Name, "OK", "Cancel");
	        popup.PopupClosed += (o, closedArgs) =>
	        {
	            if (closedArgs.Button == "OK" && !string.IsNullOrWhiteSpace(closedArgs.Text))
	                _wallet.Name = closedArgs.Text;
	        };

	        popup.Show("Text");
        }
	}
}