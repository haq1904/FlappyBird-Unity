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

    [Header("Custom Settings")]
    public float MoveSpeed;
    public float ForceMagnitude;
    public float Radius;
}

public class PacingScenario : ScriptableObject
{
    public float timeToSpawn = 2;
    public DifficultyTag DifficultyTag;
    public ObstacleGroup[] ObstacleGroups;

    public bool TryGetObstacleGroup(string groupName, out ObstacleGroup obstacleGroup)
    {
        foreach (var group in ObstacleGroups)
        {
            if (group.GroupName == groupName)
            {
                obstacleGroup = group;
                return true;
            }
        }

        obstacleGroup = default;
        return false;
    }

}
