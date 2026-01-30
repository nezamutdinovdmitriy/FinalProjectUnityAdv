using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _args;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _args = args;

            Debug.Log("Процесс регистрации сервисов на сцене геймплея!");

            container.RegisterAsSingle(CreateGameplayCycle);
            container.RegisterAsSingle(CreateSequenceGeneratorService);
            container.RegisterAsSingle(CreateGameplayInputService);
            container.RegisterAsSingle(CreateSequenceGameplay);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            GameplayInputService input = c.Resolve<GameplayInputService>();
            SequenceGameplay sequenceGameplay = c.Resolve<SequenceGameplay>();
            WalletService walletService = c.Resolve<WalletService>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();
            WinLossService winLossService = c.Resolve<WinLossService>();

            return new GameplayCycle(input, sceneSwitcherService, sequenceGameplay, _args, walletService, coroutinesPerformer, playerDataProvider, winLossService);
        }

        private static SequenceGeneratorService CreateSequenceGeneratorService(DIContainer c)
        {
            LevelConfig levelConfig = c.Resolve<ConfigsProviderService>().GetConfig<LevelsConfigs>().GetLevelConfigBy(_args.GameMode);

            return new SequenceGeneratorService(levelConfig);
        }

        private static GameplayInputService CreateGameplayInputService(DIContainer c)
        {
            return new GameplayInputService();
        }

        private static SequenceGameplay CreateSequenceGameplay(DIContainer c)
        {
            GameplayInputService input = c.Resolve<GameplayInputService>();
            SequenceGeneratorService sequenceGenerator = c.Resolve<SequenceGeneratorService>();

            return new SequenceGameplay(input, sequenceGenerator);
        }
    }
}