using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Pages;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace Atotupe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalletsView : ContentView
	{
	    private ObservableCollection<Wallet> _wallets = new ObservableCollection<Wallet>();

	    public ObservableCollection<Wallet> Wallets
	    {
	        get => _wallets;
	        set
	        {
	            _wallets = value;
	            ListOfWallets.ItemsSource = _wallets;
	        }
	    }

		public WalletsView ()
		{
			InitializeComponent ();

		    ListOfWallets.ItemsSource = _wallets;

		    ListOfWallets.ItemTapped += OnWalletTapped;
		    ListOfWallets.ItemDragging += OnItemDragging;
		}
        
	    private void OnWalletTapped(object sender, ItemTappedEventArgs args)
	    {
	        WalletSummaryPage page = Activator.CreateInstance(typeof(WalletSummaryPage)) as WalletSummaryPage;
	        Navigation.PushAsync(page);

	        ListOfWallets.SelectedItem = null;
	        if (page != null) page.Wallet = args.ItemData as Wallet;
	    }

	    private void OnItemDragging(object sender, ItemDraggingEventArgs args)
	    {
	        if (args.Action == DragAction.Drop)
	        {
	            args.Cancel = true;
	            _wallets.Move(args.OldIndex, args.NewIndex);
	        }
	    }
    }
}