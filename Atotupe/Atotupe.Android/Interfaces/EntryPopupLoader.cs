using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
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
        public void ShowPopup(EntryPopup popup, string type)
        {
            var alert = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity);

            EditText edit = null;

            switch (type)
            {
                case "Number":
                    edit = new EditText(CrossCurrentActivity.Current.Activity) { Text = popup.Text, InputType = (InputTypes.ClassNumber)};
                    break;
                case "Decimal":
                    edit = new EditText(CrossCurrentActivity.Current.Activity) { Text = popup.Text, InputType = (InputTypes.ClassNumber | InputTypes.NumberFlagDecimal)};
                    break;
                default:
                    edit = new EditText(CrossCurrentActivity.Current.Activity) { Text = popup.Text };
                    break;
            }

            alert.SetView(edit);
            alert.SetTitle(popup.Title);

            alert.SetPositiveButton(popup.Buttons[0], (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = popup.Buttons[0],
                    Text = edit.Text
                });
            });

            alert.SetNegativeButton(popup.Buttons[1], (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = popup.Buttons[1],
                    Text = edit.Text
                });
            });
            alert.Show();
        }
    }
}