using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private PlayerDataProvider _playerDataProvider;
        private PlayerStatsPresenter _playerStatsPresenter;
        private StatsResetPurchaseService _playerStatsResetPurchaseService;
        private MainMenuInputService _menuInputService;

        private ICoroutinesPerformer _coroutinesPerformer;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация меню сцены");

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _playerStatsPresenter = _container.Resolve<PlayerStatsPresenter>();
            _playerStatsResetPurchaseService = _container.Resolve<StatsResetPurchaseService>();
      
            _menuInputService = _container.Resolve<MainMenuInputService>();

            _menuInputService.ModeSelected += OnGameModeSelected;
            _menuInputService.StatsViewRequested += OnStatsViewRequested;
            _menuInputService.ResetStatsPurchaseRequested += OnResetStatsPurchaseRequested;

            yield break;
        }

        private void OnDestroy()
        {
            _menuInputService.ModeSelected -= OnGameModeSelected;
            _menuInputService.StatsViewRequested -= OnStatsViewRequested;
            _menuInputService.ResetStatsPurchaseRequested -= OnResetStatsPurchaseRequested;
        }

        public override void Run()
        {
            Debug.Log("Старт меню сцены");
        }

        private void Update()
        {
            _menuInputService?.Update();
        }

        private void OnGameModeSelected(GameModeType gameMode)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode)));
        }

        private void OnResetStatsPurchaseRequested()
        {
            _playerStatsResetPurchaseService.Reset();

            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
        }

        private void OnStatsViewRequested() => _playerStatsPresenter.Show();
    }
}
