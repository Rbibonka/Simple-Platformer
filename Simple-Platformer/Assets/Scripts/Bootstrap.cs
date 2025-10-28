using UnityEngine;

[DefaultExecutionOrder(-1)]
public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private LevelsConfig levelConfig;

    [SerializeField]
    private PlayerConfig playerConfig;

    [SerializeField]
    private MainMenuUI mainMenu;

    [SerializeField]
    private GameUI gameUI;

    [SerializeField]
    private UIFader uiFader;

    [SerializeField]
    private CameraFollower cameraFollower;

    [SerializeField]
    private EnemyConfig enemyConfig;

    private UILoopController uiLoopController;
    private GameLoop gameLoop;

    private void Awake()
    {
        uiLoopController = new(mainMenu, gameUI, uiFader);

        gameLoop = new(uiLoopController, playerConfig, enemyConfig, levelConfig, cameraFollower);
        gameLoop.Initialize();
    }

    private void OnDestroy()
    {
        gameLoop.Dispose();
    }
}