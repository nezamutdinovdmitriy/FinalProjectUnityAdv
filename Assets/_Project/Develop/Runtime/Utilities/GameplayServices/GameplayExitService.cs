using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Utilities.GameplayServices
{
    public class GameplayExitService
    {
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayInputArgs _args;

        private GameplayResult _result;

        public GameplayExitService(GameplayCycle gameplayCycle, SceneSwitcherService sceneSwitcher, ICoroutinesPerformer coroutinesPerformer, GameplayInputArgs args)
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _args = args;

            gameplayCycle.Finished += Finsihed;
            gameplayCycle.ExitConfirmed += OnExitConfirmed;
        }

        private void Finsihed(GameplayResult result)
        {
            _result = result;
        }

        private void OnExitConfirmed()
        {
            switch (_result)
            {
                case GameplayResult.Win:
                    _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
                    break;
                case GameplayResult.Lose:
                    _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, _args));
                    break;

            }
        }
    }
}