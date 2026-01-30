using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class PlayerStatsPresenter
    {
        private WalletService _walletService;
        private WinLossService _winLossService;

        public PlayerStatsPresenter(WalletService walletService, WinLossService winLossService)
        {
            _walletService = walletService;
            _winLossService = winLossService;
        }

        public void Show()
        {
            foreach (CurrencyType currency in _walletService.AvailableCurrencies)
                Debug.Log($"{currency}: {_walletService.GetCurrency(currency).Value}");

            Debug.Log($"Wins: {_winLossService.TotalWins}");
            Debug.Log($"Losses: {_winLossService.TotalLosses}");
        }
    }
}