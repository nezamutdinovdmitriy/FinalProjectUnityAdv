using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Configs.StatsReset;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню!");

            container.RegisterAsSingle(CreateGameModeSelectorService);
            container.RegisterAsSingle(CreatePlayerStatsPresenter);
            container.RegisterAsSingle(CreateStatsResetPurchaseService);
        }

        private static GameModeSelectorService CreateGameModeSelectorService(DIContainer c)
        {
            return new GameModeSelectorService();
        }

        private static PlayerStatsPresenter CreatePlayerStatsPresenter(DIContainer c)
        {
            WalletService walletService = c.Resolve<WalletService>();
            WinLossService winLossService = c.Resolve<WinLossService>();

            return new PlayerStatsPresenter(walletService, winLossService);
        }

        private static StatsResetPurchaseService CreateStatsResetPurchaseService(DIContainer c)
        {
            WalletService walletService = c.Resolve<WalletService>();
            WinLossService winLossService = c.Resolve<WinLossService>();
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            StatsResetConfig config = configsProviderService.GetConfig<StatsResetConfig>();

            return new StatsResetPurchaseService(walletService, winLossService, config);
        }
    }
}