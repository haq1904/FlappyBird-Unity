using UnityEngine;

public class BirdStateMachine
{
    public BirdState CurrentBirdState;

    public void Initialize(BirdState birdState)
    {
        CurrentBirdState = birdState;
        CurrentBirdState.EnterState();
    }

    public void ChangeState(BirdState birdState)
    {
        CurrentBirdState.ExitState();
        CurrentBirdState = birdState;
        CurrentBirdState.EnterState();
    }
}
