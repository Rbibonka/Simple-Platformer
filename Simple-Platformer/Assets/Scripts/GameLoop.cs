using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public sealed class GameLoop : IDisposable
{
    private UILoopController uiLoopController;
    private PlayerConfig playerConfig;

    private LevelsController levelController;
    private CameraFollower cameraFollower;

    private EnemyCreator enemyCreator;
    private CoinsCreator coinsCreator;

    private Player player;

    private CancellationTokenSource cts = new();

    public GameLoop(UILoopController uiLoopController, PlayerConfig playerConfig, EnemyConfig enemyConfig, LevelsConfig levelConfig, CameraFollower cameraFollower)
    {
        this.uiLoopController = uiLoopController;
        this.playerConfig = playerConfig;
        this.cameraFollower = cameraFollower;

        enemyCreator = new(enemyConfig.EnemyPrefab);
        coinsCreator = new(levelConfig.ÑoinPrefab);
        levelController = new(levelConfig, enemyCreator, coinsCreator, enemyConfig);
    }

    public void Initialize()
    {
        uiLoopController.FadeOutImmediately();
        uiLoopController.ShowMainMenuUI();
        uiLoopController.ButtonStartGameClicked += OnButtonStartGameClicked;
    }

    public void Dispose()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        uiLoopController.ButtonStartGameClicked -= OnButtonStartGameClicked;
    }

    private void OnButtonStartGameClicked()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;

        cts = new();

        LoadLevel(cts.Token).Forget();
    }

    private async UniTask LoadLevel(CancellationToken ct)
    {
        player = GameObject.Instantiate(playerConfig.PlayerPrefab);

        levelController.Reset();

        cameraFollower.Initialize(player.transform);

        while (!levelController.IsLevelsEnd)
        {
            await uiLoopController.FadeIn(ct);

            uiLoopController.ShowGameUI();
            player.Initialize(playerConfig);

            levelController.CreateLevel();

            player.gameObject.SetActive(true);
            player.transform.position = levelController.CurrentLevel.PlayerTransform.position;
            uiLoopController.InitializeGameUI(player, player.PlayerHealth, player.PlayerCoins);

            cameraFollower.CameraToTargetImmediately();
            cameraFollower.StartFollow();

            await uiLoopController.FadeOut(ct);

            await UniTask.WaitUntil(() => player.IsDead || levelController.EnemiesCount == 0
            || levelController.CurrentLevel.MinYPosition.position.y > player.transform.position.y, cancellationToken: ct);

            cameraFollower.StopFollow();

            if (levelController.EnemiesCount == 0)
            {
                player.SaveCoins();
                levelController.SetNextLevel();
            }
            else
            {
                player.ResetCoins();
                player.gameObject.SetActive(false);
            }

            await uiLoopController.FadeIn(ct);

            levelController.DeleteLevel();

            player.Deinitialize();
        }

        GameObject.Destroy(player.gameObject);

        uiLoopController.ShowMainMenuUI();
        await uiLoopController.FadeOut(ct);
    }
}