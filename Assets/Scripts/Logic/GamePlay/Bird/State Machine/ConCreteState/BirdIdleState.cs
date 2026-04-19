using UnityEngine;

public class BirdIdleState : BirdState
{
    public BirdIdleState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        bird.RB.bodyType = RigidbodyType2D.Kinematic;
        
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
