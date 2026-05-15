using UnityEngine;

public class BirdIdleState : BirdState
{
    
    public BirdIdleState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
        nameState = "Idle state";
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        GetState();
        bird.RB.bodyType = RigidbodyType2D.Kinematic;
        bird.animator.Play("Idle");
        
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

    public override void GetState()
    {
        base.GetState();
        bird.BirdState = nameState;
    }
}
