using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuInputService _menuInputService;


        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация меню сцены");

            _menuInputService = _container.Resolve<MainMenuInputService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт меню сцены");
        }

        private void Update()
        {
            _menuInputService?.Update();
        }
    }
}
