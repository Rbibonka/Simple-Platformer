using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/PlayerConfigs", order = 51)]
public sealed class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public Player PlayerPrefab { get; private set; }
}