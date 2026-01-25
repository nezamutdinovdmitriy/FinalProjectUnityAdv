using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameplayCycle : IDisposable
    {
        public event Action<GameplayResult> Finished;
        public event Action ExitConfirmed;

        private readonly GameplayInputService _input;
        private readonly SequenceGameplay _gameplay;

        private bool _awaitingConfirm;

        public GameplayCycle(GameplayInputService input, SequenceGameplay gameplay)
        {
            _input = input;
            _gameplay = gameplay;
        }

        public void StartGame()
        {
            _gameplay.Finished += OnGameplayFinished;
            _input.OnConfirm += HandleConfirm;

            _gameplay.Start();
        }

        public void Dispose()
        {
            _gameplay.Finished -= OnGameplayFinished;
            _input.OnConfirm -= HandleConfirm;
        }

        public void Update() => _input.Update();

        private void OnGameplayFinished(GameplayResult result)
        {
            _awaitingConfirm = true;
            Finished?.Invoke(result);

            Debug.Log("Для продолжения нажмите SPACE!");
        }

        private void HandleConfirm()
        {
            if (_awaitingConfirm == false)
                return;

            ExitConfirmed?.Invoke();
        }
    }
}