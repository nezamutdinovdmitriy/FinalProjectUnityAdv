using Assets._Project.Develop.Runtime.Gameplay;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices
{
    public class MainMenuInputService
    {
        public event Action ResetStatsPurchaseRequested;
        public event Action StatsViewRequested;

        private const KeyCode StatsResetPurchaseKey = KeyCode.R;
        private const KeyCode PlayerStatsMessageKey = KeyCode.S;

        public void Update()
        {
            if (Input.GetKeyDown(StatsResetPurchaseKey))
                ResetStatsPurchaseRequested?.Invoke();

            if (Input.GetKeyDown(PlayerStatsMessageKey))
                StatsViewRequested?.Invoke();
        }
    }
}