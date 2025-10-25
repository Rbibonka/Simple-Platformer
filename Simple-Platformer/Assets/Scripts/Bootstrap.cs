using UnityEngine;

[DefaultExecutionOrder(-1)]
public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private LevelConfig levelConfig;

    [SerializeField]
    private PlayerConfig playerConfig;

    [SerializeField]
    private MainMenuUI mainMenu;

    [SerializeField]
    private GameUI gameUI;

    private UILoopController uiLoopController;
    private GameLoop gameLoop;

    private void Awake()
    {
        uiLoopController = new(mainMenu, gameUI);

        var player = Instantiate(playerConfig.PlayerPrefab);
        player.Initialize();
        player.gameObject.SetActive(false);

        gameLoop = new(uiLoopController, player, levelConfig);
        gameLoop.Initialize();
    }

    private void OnDestroy()
    {
        
    }
}