using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameModeSelectorService _gameModeSelector;

        private WalletService _walletService;

        private PlayerDataProvider _playerDataProvider;

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

            _walletService = _container.Resolve<WalletService>();

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();

            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

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

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log($"Кол-во золота: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log($"Кол-во золота: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
        }

        private void OnGameModeSelected(GameMode gameMode)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode)));
        }
    }
}