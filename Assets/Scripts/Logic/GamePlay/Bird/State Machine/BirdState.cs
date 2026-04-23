using UnityEngine;

public class BirdState
{
    protected BirdBase bird;
    protected BirdStateMachine birdStateMachine;
    protected string nameState;

    protected BirdState(BirdBase bird, BirdStateMachine birdStateMachine)
    {
        this.bird = bird;
        this.birdStateMachine = birdStateMachine;
    }

    public virtual void GetState()
    {
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

    public virtual void HandleCollision(Vector2 direction,float impactForce,float gravityScale)
    {

    }
}
