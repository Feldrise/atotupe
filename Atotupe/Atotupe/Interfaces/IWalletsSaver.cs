using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Atotupe.Data;

namespace Atotupe.Interfaces
{
    public interface IWalletsSaver
    {
        Task SaveWalletAsync(Wallet wallet);
        Task<Wallet> LoadWalletAsync(int id);
    }
}
