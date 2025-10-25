using UnityEngine;

public sealed class Level : MonoBehaviour
{
    [field: SerializeField]
    public Transform PlayerTransform { get; private set; }
}