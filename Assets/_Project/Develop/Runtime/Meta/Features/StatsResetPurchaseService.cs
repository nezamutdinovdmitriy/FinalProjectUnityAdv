using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class StatsResetPurchaseService
    {
        private WalletService _walletService;
        private WinLossService _winLossService;
        
        private int _price;

        public StatsResetPurchaseService(WalletService walletService, WinLossService winLossService, int config)
        {
            _walletService = walletService;
            _winLossService = winLossService;
            _price = config;
        }

        public void Reset()
        {
            if(_walletService.Enough(CurrencyType.Gold, _price))
            {
                _walletService.Spend(CurrencyType.Gold, _price);

                _winLossService.Reset();

                Debug.Log("Статистика сброшена");
            }
            else
            {
                Debug.Log("Не достаточно монет для сброса статистики");
            }
        }
    }
}