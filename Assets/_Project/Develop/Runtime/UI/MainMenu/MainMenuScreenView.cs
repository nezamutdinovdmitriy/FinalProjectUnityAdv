using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnLevelsMenuButtonClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnLevelsMenuButtonClicked);
        }

        private void OnLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();
    }
}