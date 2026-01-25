using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Infrastructure.MetaServices;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню!");

            container.RegisterAsSingle(CreateGameModeSelectorService);
        }

        public static GameModeSelectorService CreateGameModeSelectorService(DIContainer c)
        {
            return new GameModeSelectorService();
        }
    }
}