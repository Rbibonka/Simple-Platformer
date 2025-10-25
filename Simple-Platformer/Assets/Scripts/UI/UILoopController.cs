using System;

public sealed class UILoopController : IDisposable
{
    private MainMenuUI mainMenu;
    private GameUI gameUI;

    public event Action ButtonStartGameClicked;

    public UILoopController(MainMenuUI mainMenu, GameUI gameUI)
    {
        this.mainMenu = mainMenu;
        this.gameUI = gameUI;

        this.mainMenu.Initialize();
        this.mainMenu.ButtonStartClicked += OnButtonStartGameClicked;
    }

    public void ShowMainMenuUI()
    {
        mainMenu.Show();
        gameUI.Hide();
    }

    public void ShowGameUI()
    {
        mainMenu.Hide();
        gameUI.Show();
    }

    private void OnButtonStartGameClicked()
    {
        ButtonStartGameClicked?.Invoke();
    }

    public void Dispose()
    {
        mainMenu.ButtonStartClicked -= OnButtonStartGameClicked;
    }
}