using UnityEngine;

public enum DifficultyTag
{
    Easy,
    Normal,
    Hard
}

public class PacingScenario: ScriptableObject
{
    public float duration = 10;
    public float moveSpeed = 2;
    public float timeToSpawn = 2;
    public DifficultyTag difficultyTag;
    public Obstacle[] allowedPipe;
    
}
