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
        bird.RB.gravityScale = bird.resetGravity;
        bird.Controls.Bird.Jump.started += OnFlapStarted;
        bird.COL.isTrigger = false;
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
        HandleRotation();
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
    public override void HandleCollision(Vector2 direction, float impactForce, float gravityScale)
    {
        base.HandleCollision(direction, impactForce, gravityScale);
        bird.RB.AddForce(direction * impactForce, ForceMode2D.Impulse);
        bird.RB.gravityScale = gravityScale;
        birdStateMachine.ChangeState(bird.DieState);
    }

    public override void HandleAddPoint(float point)
    {
        base.HandleAddPoint(point);
        bird.RaiseAddPointEvent(point);
    }

    public override void HandleAddCoin(float coin)
    {
        base.HandleAddCoin(coin);
        bird.RaiseAddCoinEvent(coin);
    }

    private void OnFlapStarted(InputAction.CallbackContext context)
    {
        bool isUp = true; // Mặc định là bay Lên

        // 1. Kiểm tra xem người dùng đang xài Bàn phím hay Cảm ứng/Chuột
        if (context.control.device is Keyboard)
        {
            // Nếu xài Bàn Phím: Bấm mũi tên xuống (hoặc chữ S) thì bay Xuống
            if (context.control.name == "downArrow" || context.control.name == "s")
            {
                isUp = false;
            }
        }
        else
        {
            // Nếu xài Cảm ứng/Chuột: Kiểm tra tọa độ màn hình
            if (Pointer.current != null)
            {
                Vector2 tapPosition = Pointer.current.position.ReadValue();
                if (tapPosition.x >= Screen.width / 2f)
                {
                    isUp = false; // Bấm Nửa Phải -> Bay Xuống
                }
            }
        }

        // 2. Thực hiện bay Lên hoặc Xuống
        bird.RB.linearVelocity = Vector2.zero; // Trả vận tốc về 0 trước

        if (isUp)
        {
            bird.Flap();
            bird.RB.AddForce(Vector2.up * bird.JumpForce, ForceMode2D.Impulse);
            bird.transform.rotation = Quaternion.Euler(0, 0, bird.maxUpAngle);
        }
        else
        {
            bird.Flap();
            bird.RB.AddForce(Vector2.down * (bird.JumpForce - 3), ForceMode2D.Impulse);
            bird.transform.rotation = Quaternion.Euler(0, 0, bird.maxDownAngle);
        }
    }

    private void HandleRotation()
    {
        if (bird.RB.linearVelocityY < 0)
        {
            Quaternion targetRot = Quaternion.Euler(0, 0, bird.maxDownAngle);

            bird.transform.rotation = Quaternion.RotateTowards(
                bird.transform.rotation,
                targetRot,
                bird.rotationSpeed * Time.deltaTime);
        }
    }



}
