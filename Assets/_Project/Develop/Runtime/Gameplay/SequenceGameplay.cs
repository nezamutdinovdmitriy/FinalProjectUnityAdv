using Assets._Project.Develop.Runtime.Gameplay.GameplayServices;
using System;
using System.Text;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class SequenceGameplay : IDisposable
    {
        public event Action<GameplayResult> Finished;

        private GameplayInputService _input;
        private readonly string _targetSequence;
        private readonly StringBuilder _inputBuffer = new();

        public SequenceGameplay(GameplayInputService input, SequenceGeneratorService generator)
        {
            _input = input;
            _targetSequence = generator.Generate();
        }

        public void Start()
        {
            _input.OnInput += HandleInput;
            Debug.Log($"Загаданная последовательность: {_targetSequence}");
        }

        public void Dispose()
        {
            _input.OnInput -= HandleInput;
        }

        private void HandleInput(char input)
        {
            _inputBuffer.Append(input);
            Debug.Log($"Ввод игрока: {_inputBuffer}");

            if (_inputBuffer.Length < _targetSequence.Length)
                return;

            GameplayResult result = _inputBuffer.ToString() == _targetSequence ? GameplayResult.Win : GameplayResult.Lose;

            PrintMessage(result);

            Finished?.Invoke(result);

            _input.OnInput -= HandleInput;
        }

        private void PrintMessage(GameplayResult result)
        {
            if (result == GameplayResult.Win)
                Debug.Log("Win!");
            else
                Debug.Log("Lose!");
        }
    }
}