using UnityEngine;

public class LevelsController
{
    private int currentLevelId;
    private LevelConfig levelConfig;

    private Level currentLevel;

    public Level CurrentLevel => currentLevel;

    public LevelsController(LevelConfig levelConfig)
    {
        this.levelConfig = levelConfig;
    }

    public void CreateLevel()
    {
        currentLevel = GameObject.Instantiate(levelConfig.Level[currentLevelId]);
    }

    public void DeleteLevel()
    {
        currentLevelId++;
    }
}