using UnityEngine;

public class BirdDieState : BirdState
{
    public BirdDieState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        switch (bird.LastDeathType)
        {
            case DeathType.PipeHit:
                break;
            case DeathType.CeilingHit:
                break;
            case DeathType.GroundHit:
                break;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
