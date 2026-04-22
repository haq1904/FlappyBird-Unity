using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdFlyingState : BirdState
{
    public BirdFlyingState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        bird.Controls.Bird.Jump.Enable();
        bird.RB.bodyType = RigidbodyType2D.Dynamic;
        bird.Controls.Bird.Jump.started += OnFlapStarted;
    }

    

    public override void ExitState()
    {
        base.ExitState();
        bird.Controls.Bird.Jump.started -= OnFlapStarted;
        bird.Controls.Bird.Jump.Disable();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private void OnFlapStarted(InputAction.CallbackContext context)
    {
        bird.RB.linearVelocity = Vector2.zero;
        bird.RB.AddForce(Vector2.up * bird.JumpForce, ForceMode2D.Impulse);
    }
}
