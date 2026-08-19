using System;
using UnityEngine;

public enum DifficultyTag
{
    Easy,
    Normal,
    Hard
}


[Serializable]
public struct ObstacleGroup
{
    public string GroupName;
    public ObstacleService[] Obstacles;
}

public class PacingScenario : ScriptableObject
{
    public float moveSpeed = 2;
    public float timeToSpawn = 2;
    public DifficultyTag DifficultyTag;
    public ObstacleGroup[] ObstacleGroups;

    public bool TryGetObstacleList(string groupName, out ObstacleService[] obstacles)
    {
        foreach (var group in ObstacleGroups)
        {
            if (group.GroupName == groupName)
            {
                obstacles = group.Obstacles;
                return true;
            }
        }

        obstacles = null;
        return false;
    }

}
