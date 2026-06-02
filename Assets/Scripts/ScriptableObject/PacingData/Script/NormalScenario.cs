using UnityEngine;

[CreateAssetMenu(fileName = "Normal Scenario", menuName = "Scriptable Objects/Pacing Scenario/Normal Scenario")]
public class NormalScenario : PacingScenario
{
    private void OnEnable()
    {
        difficultyTag = DifficultyTag.Normal;
    }
}
