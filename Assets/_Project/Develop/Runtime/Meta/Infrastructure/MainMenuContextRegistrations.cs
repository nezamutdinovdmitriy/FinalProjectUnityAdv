using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Configs.StatsReset;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню!");

            container.RegisterAsSingle(CreatePlayerStatsPresenter);
            container.RegisterAsSingle(CreateStatsResetPurchaseService);
            container.RegisterAsSingle(CreateMainMenuInputService);
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuScreenPresenterFactory);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPopupService);
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot root = c.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = c.Resolve<ViewsFactory>().Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, root.HUDLayer);

            MainMenuScreenPresenter presenter = c.Resolve<MainMenuScreenPresenterFactory>().CreateMainMenuScreenPresenter(view);

            return presenter;
        }

        private static MainMenuScreenPresenterFactory CreateMainMenuScreenPresenterFactory(DIContainer c)
            => new MainMenuScreenPresenterFactory(c);

        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRootPrefab = resourcesAssetsLoader.Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRootPrefab);
        }

        private static MainMenuInputService CreateMainMenuInputService(DIContainer c)
            => new MainMenuInputService();

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

            return new StatsResetPurchaseService(walletService, winLossService, config.Price);
        }
    }
}