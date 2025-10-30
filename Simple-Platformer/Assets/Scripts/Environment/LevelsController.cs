using System.Collections.Generic;
using UnityEngine;

public sealed class LevelsController
{
    private int currentLevelId;
    private LevelsConfig levelConfig;
    private EnemyConfig enemyConfig;

    private Level currentLevel;

    private EnemyCreator enemiesCreator;
    private CoinsCreator coinsCreator;

    private List<BaseEnemy> enemies;
    private List<Coin> coins;

    public Level CurrentLevel => currentLevel;

    public int EnemiesCount => enemies.Count;

    public bool IsLevelsEnd { get; private set; }

    public LevelsController(LevelsConfig levelConfig, EnemyCreator enemiesCreator, CoinsCreator coinsCreator, EnemyConfig enemyConfig)
    {
        this.levelConfig = levelConfig;
        this.enemiesCreator = enemiesCreator;
        this.coinsCreator = coinsCreator;
        this.enemyConfig = enemyConfig;
    }

    public void CreateLevel()
    {
        currentLevel = GameObject.Instantiate(levelConfig.Levels[currentLevelId]);

        enemies = enemiesCreator.CreateEntities(currentLevel.enemySpawnPoints.Length);
        coins = coinsCreator.CreateEntities(currentLevel.coinSpawnPoints.Length);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Initialize(currentLevel.enemySpawnPoints[i].position, enemyConfig);
            enemies[i].Dead += OnEnemyDead;
            enemies[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].CoinTaked += OnCoinTaked;
            coins[i].transform.position = currentLevel.coinSpawnPoints[i].position;
            coins[i].gameObject.SetActive(true);
        }
    }

    public void DeleteEntities()
    {
        foreach (var enemy in enemies)
        {
            enemy.Dead -= OnEnemyDead;
            enemy.Deinitialize();
            enemy.gameObject.SetActive(false);
        }

        foreach (var coin in coins)
        {
            coin.CoinTaked -= OnCoinTaked;
            coin.gameObject.SetActive(false);
        }

        coinsCreator.DeleteEnemies();
        enemiesCreator.DeleteEnemies();

        enemies.Clear();
        coins.Clear();
    }

    public void DeleteLevel()
    {
        DeleteEntities();
        GameObject.Destroy(currentLevel.gameObject);
    }

    public void Reset()
    {
        currentLevelId = 0;
        IsLevelsEnd = false;
    }

    public void SetNextLevel()
    {
        currentLevelId++;

        if (currentLevelId >= levelConfig.Levels.Length)
        {
            IsLevelsEnd = true;
        }
    }

    private void OnEnemyDead(BaseEnemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        enemy.gameObject.SetActive(false);
        enemy.Deinitialize();

        enemiesCreator.DeleteEntity(enemy);
        enemies.Remove(enemy);
    }

    private void OnCoinTaked(Coin coin)
    {
        coin.CoinTaked -= OnCoinTaked;
        coin.gameObject.SetActive(false);

        coinsCreator.DeleteEntity(coin);
        coins.Remove(coin);
    }
}