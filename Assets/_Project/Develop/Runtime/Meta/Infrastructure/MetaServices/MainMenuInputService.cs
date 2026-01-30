using Assets._Project.Develop.Runtime.Gameplay;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices
{
    public class MainMenuInputService
    {
        public event Action ResetStatsPurchaseRequested;
        public event Action StatsViewRequested;

        public event Action<GameModeType> ModeSelected;

        private const KeyCode StatsResetPurchaseKey = KeyCode.R;
        private const KeyCode PlayerStatsMessageKey = KeyCode.S;
        
        private const KeyCode GameModeDigitsKey = KeyCode.Alpha1;
        private const KeyCode GameModeLettersKey = KeyCode.Alpha2;

        public void Update()
        {
            if (Input.GetKeyDown(StatsResetPurchaseKey))
                ResetStatsPurchaseRequested?.Invoke();

            if (Input.GetKeyDown(PlayerStatsMessageKey))
                StatsViewRequested?.Invoke();

            if (Input.GetKeyDown(GameModeDigitsKey))
                ModeSelected?.Invoke(GameModeType.Digits);

            if (Input.GetKeyDown(GameModeLettersKey))
                ModeSelected?.Invoke(GameModeType.Letters);
        }
    }
}