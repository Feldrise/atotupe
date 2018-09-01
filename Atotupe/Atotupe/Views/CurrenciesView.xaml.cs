using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Atotupe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrenciesView : ContentView
	{
	    private ObservableCollection<Currency> _currencies;

	    public ObservableCollection<Currency> Currencies
	    {
	        get => _currencies;
	        set => _currencies = value;
	    }

        public CurrenciesView ()
		{
			InitializeComponent ();
            GenerateCurrencies();

		    ListOfWallets.ItemsSource = _currencies;
		}

	    public void AddCurrency(Currency item)
	    {
            _currencies.Add(item);
	    }

	    private void GenerateCurrencies()
	    {
            //TODO: generate from save
	        _currencies = new ObservableCollection<Currency>
	        {
                new Currency { Code = "BTC", Name = "Bitcoin", Number = 1.2, Price = 5460, Value = 5895 },
	            new Currency { Code = "BCH", Name = "Bitcoin Cash", Number = 0.3, Price = 3260, Value = 2895 },
                new Currency { Code = "LTC", Name = "Litecoin", Number = 3, Price = 1567, Value = 5321 }
	        };
	    }
    }
}