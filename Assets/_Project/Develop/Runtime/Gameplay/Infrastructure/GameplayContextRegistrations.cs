using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.GameplayServices;
using Assets._Project.Develop.Runtime.Utilities.InputManagment;
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
            container.RegisterAsSingle(CreateGameplayExitService);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            GameplayInputService input = c.Resolve<GameplayInputService>();
            SequenceGeneratorService generator = c.Resolve<SequenceGeneratorService>();

            return new GameplayCycle(input, generator);
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

        private static GameplayExitService CreateGameplayExitService(DIContainer c)
        {
            GameplayCycle cycle = c.Resolve<GameplayCycle>();
            SceneSwitcherService sceneSwitcher = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<CoroutinesPerformer>();
            
            return new GameplayExitService(cycle, sceneSwitcher, coroutinesPerformer, _args);
        }
    }
}