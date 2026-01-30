using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
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
        private GameModeSelectorService _gameModeSelector;

        private PlayerDataProvider _playerDataProvider;
        private WinLossService _winLossService;
        private PlayerStatsPresenter _playerStatsPresenter;
        private StatsResetPurchaseService _playerStatsResetPurchaseService;

        private ICoroutinesPerformer _coroutinesPerformer;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация меню сцены");

            _gameModeSelector = _container.Resolve<GameModeSelectorService>();

            _gameModeSelector.ModeSelected += OnGameModeSelected;

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();

            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _winLossService = _container.Resolve<WinLossService>();

            _playerStatsPresenter = _container.Resolve<PlayerStatsPresenter>();

            _playerStatsResetPurchaseService = _container.Resolve<StatsResetPurchaseService>();

            yield break;
        }

        private void OnDestroy()
        {
            _gameModeSelector.ModeSelected -= OnGameModeSelected;

        }

        public override void Run()
        {
            Debug.Log("Старт меню сцены");
        }

        private void Update()
        {
            _gameModeSelector?.Update();

            if (Input.GetKeyDown(KeyCode.R))
            {
                _playerStatsResetPurchaseService.Reset();
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            }    

            if (Input.GetKeyDown(KeyCode.S))
                _playerStatsPresenter.Show();
        }

        private void OnGameModeSelected(GameMode gameMode)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode)));
        }
    }
}