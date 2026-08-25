using UnityEngine;

public class BirdDieState : BirdState
{
    public BirdDieState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
        nameState = "Die state";
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
        bird.COL.isTrigger = true;
        bird.PlayAnimationClip("Die");
    }

    public override void ExitState()
    {
        base.ExitState();
        bird.StopCrashSmokePuffPS();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (bird.transform.position.y < -15)
        {
            bird.RB.bodyType = RigidbodyType2D.Kinematic;
            bird.transform.position = new Vector3(bird.transform.position.x, -14, bird.transform.position.z);
            bird.RB.linearVelocity = Vector2.zero;
            bird.RB.angularVelocity = 0f;
        }


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


}
