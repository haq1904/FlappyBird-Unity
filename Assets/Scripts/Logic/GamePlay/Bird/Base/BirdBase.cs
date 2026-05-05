
using UnityEngine;


public class BirdBase : MonoBehaviour, IDamageable, IReceivable
{
    [field:SerializeField] public float JumpForce { get; set; }
    [SerializeField] private SoundTypeGameEvent OnBirdRaiseSoundEvent;
    [SerializeField] private FloatGameEvent OnBirdRaisePoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
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

    private void Update()
    {
        StateMachine.CurrentBirdState.FrameUpdate();
    }

    #endregion

    #region Received Event Function
    public void HandleFlying() //Receive OnStartFlying event from EasyModeManager
    {
        StateMachine.ChangeState(FlyingState);
    }
    #endregion

    #region IDamageable function
    public void PlayAnimation(DamageableAnimationStandard animation)
    {
        
    }

    public void TakeDamage(Vector2 direction,float impactForce, float gravityScale)
    {
        StateMachine.CurrentBirdState.HandleCollision(direction, impactForce, gravityScale);
    }

    #endregion

    #region IReceivable function
    public void PlayAnimation(ReceivableAnimationStandard animationName)
    {
        
    }

    public void AddPoint(float point)
    {
        StateMachine.CurrentBirdState.HandleAddPoint(point);
    }
    #endregion

    #region Control function

    public void Flap()
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.Flap);
         
    }


    public void RaiseAddPointEvent(float point)
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.TakePoint);
        OnBirdRaisePoint.Raise(point);
    }


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
