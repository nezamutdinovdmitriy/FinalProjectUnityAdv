using Assets._Project.Develop.Runtime.Meta.Features;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}