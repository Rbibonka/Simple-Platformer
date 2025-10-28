using UnityEngine;

public sealed class Level : MonoBehaviour
{
    [field: SerializeField]
    public Transform PlayerTransform { get; private set; }

    [field: SerializeField]
    public Transform MinYPosition { get; private set; }

    [field: SerializeField]
    public Transform[] enemySpawnPoints { get; private set; }

    [field: SerializeField]
    public Transform[] coinSpawnPoints { get; private set; }
}