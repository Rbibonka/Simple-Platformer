using System;

public class GameLoop : IDisposable
{
    private UILoopController uiLoopController;
    private Player player;
    private LevelConfig levelConfig;

    private LevelsController levelController;

    public GameLoop(UILoopController uiLoopController, Player player, LevelConfig levelConfig)
    {
        this.uiLoopController = uiLoopController;
        this.player = player;
        this.levelConfig = levelConfig;

        levelController = new(this.levelConfig);
    }

    public void Initialize()
    {
        uiLoopController.ShowMainMenuUI();
        uiLoopController.ButtonStartGameClicked += OnButtonStartGameClicked;
    }

    public void Dispose()
    {
        uiLoopController.ButtonStartGameClicked -= OnButtonStartGameClicked;
    }

    private void OnButtonStartGameClicked()
    {
        player.gameObject.SetActive(true);
        LoadLevel();
    }

    private void LoadLevel()
    {
        uiLoopController.ShowGameUI();
        levelController.CreateLevel();
        player.transform.position = levelController.CurrentLevel.PlayerTransform.position;
    }
}