using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/PlayerConfigs", order = 51)]
public sealed class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public Player PlayerPrefab { get; private set; }

    [field: SerializeField]
    public int PlayerHealth { get; private set; }

    [field: SerializeField]
    public int PlayerDamage { get; private set; }

    [field: SerializeField]
    public int PlayerSpeed { get; private set; }

    [field: SerializeField]
    public int PlayerJumpForce { get; private set; }

    [field: SerializeField]
    public int PlayerSmallJumpForce { get; private set; }
}