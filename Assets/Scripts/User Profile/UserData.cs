using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UserData
{

    public UserData()
    {
        levelData = new Dictionary<string, LevelCompletionData>();
        levelData["Level1"] = new LevelCompletionData();
        Debug.Log("Level1 has a best completion of " + levelData["Level1"].bestPerformance);
    }

    Dictionary<string, LevelCompletionData> levelData;


    public void UpdateLevelData(string toUpdate, int newBest)
    {

        Debug.Log("updating level " + toUpdate + ", new best is " + newBest);

        levelData[toUpdate].UpdateBest(newBest);


        foreach (LevelCompletionData currData in levelData.Values)
        {
            currData.canSelect = currData.CheckSelectable();
        }
    }
    
    public int GetStarsForLevel(string levelName)
    {
        return levelData[levelName].bestPerformance;
    }
}

[System.Serializable]
public class LevelCompletionData
{
    public int bestPerformance;
    public bool canSelect;
    List<LevelCompletionData> requirements;

    public LevelCompletionData()
    {
        canSelect = false;
        bestPerformance = 0;
        requirements = new List<LevelCompletionData>();
    }

    public bool CheckSelectable()
    {
        foreach (LevelCompletionData curr in requirements)
        {
            if (curr.bestPerformance == 0)
            {
                return false;
            }
        }

        return true;
    }

    public void UpdateBest(int newBest)
    {
        bestPerformance = Mathf.Max(newBest, bestPerformance, 3);
    }
}


