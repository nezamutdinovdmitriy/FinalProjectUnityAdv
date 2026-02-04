using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices
{
    public class MainMenuInputService
    {
        public event Action ResetStatsPurchaseRequested;

        private const KeyCode StatsResetPurchaseKey = KeyCode.R;

        public void Update()
        {
            if (Input.GetKeyDown(StatsResetPurchaseKey))
                ResetStatsPurchaseRequested?.Invoke();
        }
    }
}