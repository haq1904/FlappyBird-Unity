using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdBase : MonoBehaviour, IDamageable, IMoveable
{
    [field:SerializeField] public float Force { get; set; }
    public Rigidbody2D RB { get; set; }

    private BirdControls Controls;

    #region State Machine Variables
    public BirdStateMachine StateMachine { get; set; }
    public BirdIdleState IdleState { get; set; }
    public BirdFlyingState FlyingState { get; set; }
    public BirdPauseState PauseState { get; set; }
    public BirdDieState DieState { get; set; }
    #endregion

    private void Awake()
    {
        StateMachine = new BirdStateMachine();
        IdleState = new BirdIdleState(this, StateMachine);
        FlyingState = new BirdFlyingState(this, StateMachine);
        PauseState = new BirdPauseState(this, StateMachine);
        DieState = new BirdDieState(this, StateMachine);
        Controls = new BirdControls();
        
    }

    private void OnEnable()
    {
        Controls.Enable();
        Controls.Bird.Jump.started += OnFlapStarted;
    }

   

    private void OnDisable()
    {
        Controls.Disable();
    }

    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
    }

    public void HandleFlying()
    {
        Debug.Log("MainBird received OnStartFlying event");
        Debug.Log("Change state : Idle - > Flying");
        StateMachine.ChangeState(FlyingState);
    }


    #region Die function
    public void Die()
    {
        
    }
    #endregion

    #region Control function
    private void OnFlapStarted(InputAction.CallbackContext context)
    {
        Flap();
    }
    public void Flap()
    {
        Debug.Log("Player pressed space button.");
    }

    public void ResetVelocity()
    {
        
    }
    #endregion

    #region Animation Trigger
    public void AnimationTriggerEvent(AnimationTriggerType triggerType) {
        StateMachine.CurrentBirdState.AnimationTriggerEvent(triggerType);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public enum AnimationTriggerType
    {
        Example
    }
    #endregion
}
