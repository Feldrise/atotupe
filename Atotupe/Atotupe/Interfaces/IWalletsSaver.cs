using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;

namespace Atotupe.Interfaces
{
    public interface IWalletsSaver
    {
        Task SaveWalletAsync(string walletJson);
        Task<ObservableCollection<Wallet>> LoadWalletsAsync();
    }
}
