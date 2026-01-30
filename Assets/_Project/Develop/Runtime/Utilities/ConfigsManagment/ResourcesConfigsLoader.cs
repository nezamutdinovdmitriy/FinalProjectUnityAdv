using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Meta.Configs.StatsReset;
using Assets._Project.Develop.Runtime.Meta.Configs.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPath = new()
    {
            {typeof(LevelsConfigs), "Configs/Gameplay/LevelsConfigs" },
            {typeof(StartWalletConfig), "Configs/Meta/StartWalletConfig" },
            {typeof(StatsResetConfig), "Configs/Meta/StatsResetConfig" },
            {typeof(CurrencyIconsConfig), "Configs/Meta/CurrencyIconsConfig" }
    };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configResourcePath in _configsResourcesPath)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcePath.Value);

                loadedConfigs.Add(configResourcePath.Key, config);

                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}