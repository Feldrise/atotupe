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
		    _wallet = new Wallet {Name = "Wallet 1"};

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