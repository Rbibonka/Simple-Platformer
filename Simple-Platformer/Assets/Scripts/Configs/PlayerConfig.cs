using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/PlayerConfigs", order = 51)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public Player Player { get; private set; }
}