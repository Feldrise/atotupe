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

	            popup.Show("Number");
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
    }
}