using System.Collections;

public class BirdDieState : BirdState
{
    public BirdDieState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
        nameState="Die state";
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        GetState();
        bird.BirdDead();
        bird.animator.Play("Die", -1, 0f);
    }

    public override void ExitState()  
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void GetState()
    {
        base.GetState();
        bird.BirdState = nameState;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void HandleTrigger()
    {
        base.HandleTrigger();
        bird.gameObject.SetActive(false);
    }


}
