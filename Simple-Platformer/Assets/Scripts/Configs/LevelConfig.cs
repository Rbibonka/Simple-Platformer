using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/LevelConfigs", order = 51)]
public sealed class LevelConfig : ScriptableObject
{
    [field: SerializeField]
    public Level[] Level { get; private set; }
}