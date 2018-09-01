using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
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
	        set => _wallet = value;
	    }

		public WalletSummaryPage ()
		{
			InitializeComponent ();

            //TODO: TEMP
		    var currencies = new ObservableCollection<Currency>
		    {
		        new Currency { Code = "BTC", Name = "Bitcoin", Number = 1.2, Price = 5460, Value = 5895 },
		        new Currency { Code = "BCH", Name = "Bitcoin Cash", Number = 0.3, Price = 3260, Value = 2895 },
		        new Currency { Code = "LTC", Name = "Litecoin", Number = 3, Price = 1567, Value = 5321 }
		    };

            _wallet = new Wallet {Name = "Wallet 1", Currencies = currencies};

            // Register events
		    AddCurrency.Clicked += OnAddCurrency;
		}

	    private async void OnAddCurrency(object sender, EventArgs args)
	    {
	        var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Bitcoin", "Bitcoin Cash", "Ethereum", "Litecoin", "Ripple");

	        if (action != "Cancel")
	        {
	            var item = new Currency
	            {
	                Name = action,
	                Code = App.GetCodeOfCurrency(action),
	                Number = 0,
	                Price = 123,
	                Value = 0
	            };

	            if (!_wallet.ContainsCurrency(item))
	            {
                    _wallet.AddCurrency(item);
	                Currencies.AddCurrency(item);
                }
	        }
        }
	}
}