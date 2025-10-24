using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private PlayerConfig playerConfig;

    private void Awake()
    {
        var gameInitialize = new GameInitializer(playerConfig);
    }
}