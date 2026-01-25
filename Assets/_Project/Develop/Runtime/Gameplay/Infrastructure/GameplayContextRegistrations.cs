using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
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
            container.RegisterAsSingle(CreateSequenceGameplay);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            GameplayInputService input = c.Resolve<GameplayInputService>();
            SequenceGameplay sequenceGameplay = c.Resolve<SequenceGameplay>();

            return new GameplayCycle(input, sequenceGameplay);
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
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            
            return new GameplayExitService(cycle, sceneSwitcher, coroutinesPerformer, _args);
        }

        private static SequenceGameplay CreateSequenceGameplay(DIContainer c)
        {
            GameplayInputService input = c.Resolve<GameplayInputService>();
            SequenceGeneratorService sequenceGenerator = c.Resolve<SequenceGeneratorService>();

            return new SequenceGameplay(input, sequenceGenerator);
        }
    }
}