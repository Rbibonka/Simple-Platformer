using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Configs/EnemiesConfigs", order = 51)]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField]
    public BaseEnemy EnemyPrefab { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public int Health { get; private set; }
}