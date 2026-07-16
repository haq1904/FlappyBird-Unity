using CodiceApp;
using DG.Tweening;
using UnityEngine;

public class BirdResetState : BirdState
{
    public BirdResetState(BirdBase bird, BirdStateMachine birdStateMachine) : base(bird, birdStateMachine)
    {
        nameState = "Reset State";
    }

    public override void EnterState()
    {
        HandleReset();
    }

    public override void FrameUpdate()
    {

    }

    public override void ExitState()
    {
        bird.animator.Rebind();
        bird.SPRITE.color = Color.white;
        bird.transform.DOKill();
    }

    private void HandleReset()
    {
        bird.transform.DOKill();
        bird.animator.Play("Revive");
        bird.RB.linearVelocity = Vector2.zero;
        bird.RB.angularVelocity = 0f;
        bird.transform.rotation = Quaternion.identity;
        bird.RB.bodyType = RigidbodyType2D.Kinematic;
        bird.transform.DOMove(bird.resetPos, 4f).SetEase(Ease.OutBack, 2f).SetLink(bird.gameObject);
    }
}