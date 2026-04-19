using UnityEngine;

public class BirdState
{
    protected BirdBase bird;
    protected BirdStateMachine birdStateMachine;

    protected BirdState(BirdBase bird, BirdStateMachine birdStateMachine)
    {
        this.bird = bird;
        this.birdStateMachine = birdStateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {

    }

    public virtual void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {

    }
}
