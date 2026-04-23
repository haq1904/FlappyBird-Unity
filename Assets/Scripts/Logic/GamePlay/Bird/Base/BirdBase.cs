using Codice.Client.BaseCommands;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdBase : MonoBehaviour, IDamageable
{
    [field:SerializeField] public float JumpForce { get; set; }
     public string BirdState;
    public Rigidbody2D RB { get; set; }

    public BirdControls Controls;

    

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
    public void PlayAnimation(AnimationStandard animation)
    {
        
    }

    public void TakeDamage(Vector2 direction,float impactForce, float gravityScale)
    {
        StateMachine.CurrentBirdState.HandleCollision(direction, impactForce, gravityScale);
    }

    #endregion

    #region Control function




    #endregion

    #region Animation Trigger
    public void AnimationTriggerEvent(AnimationTriggerType triggerType) {
        StateMachine.CurrentBirdState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        Example
    }
    #endregion
}
