using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atotupe.iOS.Interfaces;
using Atotupe.Interfaces;
using Atotupe.Tools;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(EntryPopupLoader))]
namespace Atotupe.iOS.Interfaces
{
    public class EntryPopupLoader : IEntryPopupLoader
    {
        public void ShowPopup(EntryPopup popup, string type)
        {
            var alert = new UIAlertView
            {
                Title = popup.Title,
                Message = popup.Text,
                AlertViewStyle = UIAlertViewStyle.PlainTextInput
            };
            foreach (var b in popup.Buttons)
                alert.AddButton(b);

            alert.Clicked += (s, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = popup.Buttons.ElementAt(Convert.ToInt32(args.ButtonIndex)),
                    Text = alert.GetTextField(0).Text
                });
            };
            alert.Show();
        }
    }
}