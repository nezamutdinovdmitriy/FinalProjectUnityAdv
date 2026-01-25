using Assets._Project.Develop.Runtime.Gameplay;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices
{
    public class GameModeSelectorService
    {
        public event Action<GameMode> ModeSelected;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ModeSelected?.Invoke(GameMode.Digits);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ModeSelected?.Invoke(GameMode.Letters);
        }
    }
}