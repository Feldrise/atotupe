using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Atotupe.Droid.Interfaces;
using Atotupe.Interfaces;
using Atotupe.Tools;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(EntryPopupLoader))]
namespace Atotupe.Droid.Interfaces
{
    public class EntryPopupLoader : IEntryPopupLoader
    {
        public void ShowPopup(EntryPopup popup)
        {
            var alert = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity);

            var edit = new EditText(CrossCurrentActivity.Current.Activity) { Text = popup.Text };
            alert.SetView(edit);

            alert.SetTitle(popup.Title);

            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "OK",
                    Text = edit.Text
                });
            });

            alert.SetNegativeButton("Annuleren", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "Annuleren",
                    Text = edit.Text
                });
            });
            alert.Show();
        }
    }
}