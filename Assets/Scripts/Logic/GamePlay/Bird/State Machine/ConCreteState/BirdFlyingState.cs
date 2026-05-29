using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdFlyingState : BirdState
{
    private string name = "Flying state";
    public BirdFlyingState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
        nameState = "Flying state";
    }

    public override void AnimationTriggerEvent(BirdBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        GetState();
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
    public override void GetState()
    {
        base.GetState();
        bird.BirdState = name;
    }
    public override void HandleCollision(Vector2 direction, float impactForce,float gravityScale)
    {
        base.HandleCollision(direction,impactForce, gravityScale);
        bird.RB.AddForce(direction * impactForce, ForceMode2D.Impulse);
        bird.RB.gravityScale = gravityScale;
        birdStateMachine.ChangeState(bird.DieState);
    }

    public override void HandleAddPoint(float point)
    {
        base.HandleAddPoint(point);
        bird.RaiseAddPointEvent(point);
    }

    private void OnFlapStarted(InputAction.CallbackContext context)
    {
        bird.PlayDustPS();
        bird.animator.Play("Flap",-1,0f);
        bird.Flap();
        bird.RB.linearVelocity = Vector2.zero;
        bird.RB.AddForce(Vector2.up * bird.JumpForce, ForceMode2D.Impulse);
        
    }



}
