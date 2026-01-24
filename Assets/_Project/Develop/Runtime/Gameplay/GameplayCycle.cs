using Assets._Project.Develop.Runtime.Utilities.GameplayServices;
using Assets._Project.Develop.Runtime.Utilities.InputManagment;
using System;
using System.Text;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameplayCycle
    {
        public event Action<GameplayResult> Finished;
        public event Action ExitConfirmed;

        private readonly GameplayInputService _input;
        private readonly string _targetSequence;
        private readonly StringBuilder _inputBuffer = new();

        private State _state = State.Playing;

        private enum State
        {
            Playing,
            AwaitingConfirm
        }

        public GameplayCycle(GameplayInputService input, SequenceGeneratorService generatorService)
        {
            _input = input;
            _targetSequence = generatorService.Generate();
        }

        public void StartGame()
        {
            _input.OnInput += HandleInput;
            _input.OnConfirm += HandleConfirm;

            Debug.Log($"Загаданная последовательность: {_targetSequence}");
        }

        public void Update() => _input.Update();

        private void HandleInput(char input)
        {
            if (_state != State.Playing)
                return;

            _inputBuffer.Append(input);
            Debug.Log($"Ввод игрока: {_inputBuffer}");

            if (_inputBuffer.Length < _targetSequence.Length)
                return;

            FinishGame();
        }

        private void FinishGame()
        {
            _state = State.AwaitingConfirm;

            _input.OnInput -= HandleInput;

            GameplayResult result = _inputBuffer.ToString() == _targetSequence ? GameplayResult.Win : GameplayResult.Lose;

            if (result == GameplayResult.Win)
                Debug.Log("Win");
            else
                Debug.Log("Lose");

            Finished?.Invoke(result);

            Debug.Log("Для продолжения нажмите SPACE!");
        }

        private void HandleConfirm()
        {
            if (_state != State.AwaitingConfirm)
                return;

            _input.OnConfirm -= HandleConfirm;

            ExitConfirmed?.Invoke();
        }
    }
}