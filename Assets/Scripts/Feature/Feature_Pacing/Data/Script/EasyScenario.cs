using UnityEngine;

[CreateAssetMenu(fileName = "Easy Scenario", menuName = "Scriptable Objects/Pacing Scenario/Easy Scenario")]
public class EasyScenario : PacingScenario
{
    private void OnEnable()
    {
        DifficultyTag = DifficultyTag.Easy;
    }
}
