using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : ScriptableObject
{
    Dictionary<int, LevelCompletionData> levelData;


    public void UpdateLevelData()
    {
        foreach (LevelCompletionData currData in levelData.Values)
        {
            currData.canSelect = currData.CheckSelectable();
        }
    }   
}

public class LevelCompletionData
{
    int bestPerformance = 0;
    public bool canSelect = false;
    LevelCompletionData[] requirements;

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
}


