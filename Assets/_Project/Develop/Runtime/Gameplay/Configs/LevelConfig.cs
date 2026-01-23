using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; } 
    }
}