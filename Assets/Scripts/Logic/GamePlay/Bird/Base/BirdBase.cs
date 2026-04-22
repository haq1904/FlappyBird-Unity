using Codice.Client.BaseCommands;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdBase : MonoBehaviour, IDamageable
{
    [field:SerializeField] public float JumpForce { get; set; }
    public Rigidbody2D RB { get; set; }

    public BirdControls Controls;

    public DeathType LastDeathType { get; set; }

    #region State Machine Variables
    public BirdStateMachine StateMachine { get; set; }
    public BirdIdleState IdleState { get; set; }
    public BirdFlyingState FlyingState { get; set; }
    public BirdPauseState PauseState { get; set; }
    public BirdDieState DieState { get; set; }
    #endregion

    #region Mono behavior function
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
       
    }

    private void OnDisable()
    {
        if (StateMachine.CurrentBirdState != null)
            StateMachine.CurrentBirdState.ExitState();
    }

    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
    }

    #endregion

    #region Received Event Function
    public void HandleFlying() //Receive OnStartFlying event from EasyModeManager
    {
        StateMachine.ChangeState(FlyingState);
    }
    #endregion

    #region IDamageable function
    public void OnHitSomething()
    {
        
    }
    #endregion

    #region Control function
    private void OnFlapStarted(InputAction.CallbackContext context)
    {
    }

    public void ResetVelocity()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDeadly>(out IDeadly ideadly))
        { 
            LastDeathType = ideadly.GetDeathType();
            StateMachine.ChangeState(DieState);
        }
        else
        {
            Debug.Log("Game object's interface is not a type in DeathType ");
        }
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
