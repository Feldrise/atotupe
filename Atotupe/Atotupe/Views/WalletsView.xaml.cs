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
	public partial class WalletsView : ContentView
	{
	    private ObservableCollection<Wallet> _wallets;

	    public ObservableCollection<Wallet> Wallets
	    {
	        get => _wallets;
	        set => _wallets = value;
	    }

		public WalletsView ()
		{
			InitializeComponent ();
            GenerateWallets();

		    ListOfWallets.ItemsSource = _wallets;
		}

	    private void GenerateWallets()
	    {
            //TODO: generate from save
	        _wallets = new ObservableCollection<Wallet>
	        {
	            new Wallet {Name = "Wallet 1"},
	            new Wallet {Name = "Wallet 2"},
	            new Wallet {Name = "Wallet 3"}
	        };
	    }
	}
}