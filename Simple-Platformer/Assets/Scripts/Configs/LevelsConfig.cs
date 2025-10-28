using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/LevelConfigs", order = 51)]
public sealed class LevelsConfig : ScriptableObject
{
    [field: SerializeField]
    public Level[] Levels { get; private set; }

    [field: SerializeField]
    public Coin ÑoinPrefab { get; private set; }
}