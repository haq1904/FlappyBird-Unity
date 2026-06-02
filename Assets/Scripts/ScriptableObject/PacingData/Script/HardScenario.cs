using UnityEngine;

[CreateAssetMenu(fileName = "Hard Scenario", menuName = "Scriptable Objects/Pacing Scenario/Hard Scenario")]
public class HardScenario : PacingScenario
{
    private void OnEnable()
    {
        difficultyTag = DifficultyTag.Hard;
    }
}
