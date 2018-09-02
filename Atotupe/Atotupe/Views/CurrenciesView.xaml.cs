using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;
using Atotupe.Tools;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace Atotupe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrenciesView : ContentView
	{
	    private ObservableCollection<Currency> _currencies;
	    private int _currentIndex = -1;

        public ObservableCollection<Currency> Currencies
	    {
	        get => _currencies;
	        set
	        {
	            _currencies = value;
	            ListOfCurrencies.ItemsSource = _currencies;
	        }
	    }

        public CurrenciesView ()
		{
			InitializeComponent ();

		    _currencies = new ObservableCollection<Currency>();

            // Register events
		    ListOfCurrencies.ItemsSource = _currencies;
		    ListOfCurrencies.ItemTapped += OnItemSelected;
		    ListOfCurrencies.ItemDragging += OnItemDragging;
		    ListOfCurrencies.SwipeStarted += OnSwipeStarted;
		    ListOfCurrencies.SwipeEnded += OnSwipeEnded;
        }

        private void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            if (args.ItemData is Currency currentItem)
	        {
	            var popup = new EntryPopup("Number of " + currentItem.Name, currentItem.Number.ToString(), "OK", "Cancel");
	            popup.PopupClosed += (o, closedArgs) =>
	            {
	                if (closedArgs.Button == "OK")
	                    currentItem.Number = double.Parse(closedArgs.Text);

	            };

	            popup.Show("Decimal");
            }
        }

	    private void OnItemDragging(object sender, ItemDraggingEventArgs args)
	    {
	        if (args.Action == DragAction.Drop)
	        {
	            args.Cancel = true;
                _currencies.Move(args.OldIndex, args.NewIndex);
	        }
	    }

	    private void OnSwipeStarted(object sender, SwipeStartedEventArgs args)
	    {
	        _currentIndex = -1;
	    }

	    private void OnSwipeEnded(object sender, SwipeEndedEventArgs args)
	    {
	        _currentIndex = args.ItemIndex;
	    }

	    private void Delete_OnClicked(object sender, EventArgs args)
	    {
	        if (_currentIndex >= 0)
	        {
	            _currencies.RemoveAt(_currentIndex);
	        }

	        ListOfCurrencies.ResetSwipe();
	    }
    }
}