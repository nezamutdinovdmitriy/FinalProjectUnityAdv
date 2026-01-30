using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using Assets._Project.Develop.Runtime.Meta.Configs.StatsReset;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class StatsResetPurchaseService
    {
        private WalletService _walletService;
        private WinLossService _winLossService;
        private StatsResetConfig _config;

        public StatsResetPurchaseService(WalletService walletService, WinLossService winLossService, StatsResetConfig config)
        {
            _walletService = walletService;
            _winLossService = winLossService;
            _config = config;
        }

        public void Reset()
        {
            if(_walletService.Enough(CurrencyTypes.Gold, _config.Price))
            {
                _walletService.Spend(CurrencyTypes.Gold, _config.Price);

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