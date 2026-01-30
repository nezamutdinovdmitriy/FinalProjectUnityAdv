using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelsConfigs", fileName = "LevelsConfigs")]
    public class LevelsConfigs : ScriptableObject
    {
        [SerializeField] private List<Configs> _configs;

        public LevelConfig GetLevelConfigBy(GameModeType gameMode) => _configs.First(config => config.GameMode == gameMode).LevelConfig;

        [Serializable]
        private class Configs
        {
            [field: SerializeField] public GameModeType GameMode {  get; private set; }
            [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        }
    }
}