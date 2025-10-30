using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public sealed class UILoopController : IDisposable
{
    private MainMenuUI mainMenu;
    private GameUI gameUI;
    private UIFader uiFader;

    private Player player;

    public event Action ButtonStartGameClicked;

    public UILoopController(MainMenuUI mainMenu, GameUI gameUI, UIFader uiFader)
    {
        this.mainMenu = mainMenu;
        this.gameUI = gameUI;
        this.uiFader = uiFader;

        this.mainMenu.Initialize();
        this.mainMenu.ButtonStartClicked += OnButtonStartGameClicked;
    }

    public void InitializeGameUI(Player player, int startHealth, int startCoins)
    {
        this.player = player;

        OnPlayerHealthChanged(startHealth);
        OnPlayerCoinsChanged(startCoins);

        this.player.HealthChanged += OnPlayerHealthChanged;
        this.player.CoinsChanged += OnPlayerCoinsChanged;
    }

    public void ShowMainMenuUI()
    {
        mainMenu.EnableButtonsUI();
        mainMenu.Show();
        gameUI.Hide();
    }

    public void ShowGameUI()
    {
        mainMenu.Hide();
        gameUI.Show();
    }

    public void FadeInImmediately()
    {
        uiFader.FadeInImmediately();
    }

    public void FadeOutImmediately()
    {
        uiFader.FadeOutImmediately();
    }

    public async UniTask FadeIn(CancellationToken ct)
    {
        await uiFader.FadeIn(ct);
    }

    public async UniTask FadeOut(CancellationToken ct)
    {
        await uiFader.FadeOut(ct);
    }

    private void OnButtonStartGameClicked()
    {
        mainMenu.DisableButtonsUI();
        ButtonStartGameClicked?.Invoke();
    }

    private void OnPlayerCoinsChanged(int value)
    {
        gameUI.ChangeCoinsValue(value);
    }

    private void OnPlayerHealthChanged(int value)
    {
        gameUI.ChangeHealthValue(value);
    }

    public void Dispose()
    {
        player.HealthChanged -= OnPlayerHealthChanged;
        mainMenu.ButtonStartClicked -= OnButtonStartGameClicked;
        player.CoinsChanged -= OnPlayerCoinsChanged;
    }
}