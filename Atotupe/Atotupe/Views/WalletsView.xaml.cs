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
        private int _currentIndex = -1;

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
		    ListOfWallets.SwipeStarted += OnSwipeStarted;
		    ListOfWallets.SwipeEnded += OnSwipeEnded;
		}
        
	    private void OnWalletTapped(object sender, ItemTappedEventArgs args)
	    {
	        WalletSummaryPage page = Activator.CreateInstance(typeof(WalletSummaryPage)) as WalletSummaryPage;
	        Navigation.PushAsync(page);
            
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

	    private void OnSwipeStarted(object sender, SwipeStartedEventArgs args)
	    {
	        _currentIndex= -1;
	    }

	    private void OnSwipeEnded(object sender, SwipeEndedEventArgs args)
	    {
	        _currentIndex = args.ItemIndex;
	    }

        private void Delete_OnClicked(object sender, EventArgs args)
	    {
	        if (_currentIndex >= 0)
	        {
                _wallets.RemoveAt(_currentIndex);
	        }

	        ListOfWallets.ResetSwipe();
	    }
	}
}