using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Atotupe.Data;
using Atotupe.Droid.Interfaces;
using Atotupe.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(WalletsSaver))]
namespace Atotupe.Droid.Interfaces
{
    class WalletsSaver : IWalletsSaver
    {
        public async Task SaveWalletAsync(string walletJson)
        {
            var path = CreatePathToFile();
            using (StreamWriter sw = System.IO.File.CreateText(path))
                await sw.WriteAsync(walletJson);
        }

        public Task<ObservableCollection<Wallet>> LoadWalletsAsync()
        {
            ObservableCollection<Wallet> wallets = new ObservableCollection<Wallet>();
            var path = CreatePathToFile();

            if (!System.IO.File.Exists(path))
                return Task.FromResult(wallets);

            string walletsJson = "";
            using (StreamReader sr = System.IO.File.OpenText(path))
                walletsJson = sr.ReadToEndAsync().GetAwaiter().GetResult();

            if (String.IsNullOrWhiteSpace(walletsJson))
                return Task.FromResult(wallets);

            wallets = JsonConvert.DeserializeObject<ObservableCollection<Wallet>>(walletsJson);
            return Task.FromResult(wallets);
        }

        private string CreatePathToFile()
        {
            var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, "wallets");
        }
    }
}