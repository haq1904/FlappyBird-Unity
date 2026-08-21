
using System.Collections;
using System.Drawing;
using Cinemachine;
using UnityEngine;


public class BirdBase : PlayerService, IDamageable, IReceivable
{
    [Header("Fields")]
    [field: SerializeField] public float JumpForce { get; set; }
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] public Animator Animator;
    [SerializeField] private CharacterDataBaseService _characterDB;
    [SerializeField] private ParticleSystem dustPS;
    [SerializeField] private ParticleSystem _explosionPS;
    [SerializeField] private ParticleSystem _crashSmokePuffPS;
    [SerializeField] public float maxUpAngle = 18;
    [SerializeField] public float maxDownAngle = -40;
    [SerializeField] public float rotationSpeed = 15f;
    [SerializeField] private Material _whiteMaterial;


    [Header("Events")]
    [SerializeField] private SoundTypeGameEvent OnBirdRaiseSoundEvent;
    [SerializeField] private FloatGameEvent OnBirdRaisePoint;
    [SerializeField] private FloatGameEvent OnBirdRaiseCoin;
    [SerializeField] private GameEvent OnBirdDead;
    [SerializeField] private CineMachineImpulseSourceEvent OnBirdRaiseImpulseSource;


    [HideInInspector] public string BirdState;
    [HideInInspector] public Vector3 resetPos;
    [HideInInspector] public float resetGravity;


    public Rigidbody2D RB { get; set; }

    public CapsuleCollider2D COL { get; set; }

    public SpriteRenderer SPRITE { get; set; }

    public BirdControls Controls;

    private DataManagerService _dataManager;

    private Material _resetMaterial;



    #region State Machine Variables
    public BirdStateMachine StateMachine { get; set; }
    public BirdIdleState IdleState { get; set; }
    public BirdResetState ResetState { get; set; }
    public BirdFlyingState FlyingState { get; set; }
    public BirdPauseState PauseState { get; set; }
    public BirdDieState DieState { get; set; }
    #endregion

    #region Mono behavior function
    private void Awake()
    {
        StateMachine = new BirdStateMachine();
        IdleState = new BirdIdleState(this, StateMachine);
        ResetState = new BirdResetState(this, StateMachine);
        FlyingState = new BirdFlyingState(this, StateMachine);
        PauseState = new BirdPauseState(this, StateMachine);
        DieState = new BirdDieState(this, StateMachine);
        Controls = new BirdControls();
        if (_characterDB == null)
        {
            Debug.Log("Can not get charater database.");
            return;
        }
        _dataManager = FindAnyObjectByType<DataManagerService>();
        if (_dataManager == null)
        {
            Debug.Log("Can not get data manager.");
            return;
        }
        Animator.runtimeAnimatorController = _characterDB.GetCharacterById(_dataManager.GetSelectedSkinId()).AnimController;

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
        COL = gameObject.GetComponent<CapsuleCollider2D>();
        SPRITE = gameObject.GetComponent<SpriteRenderer>();
        _resetMaterial = SPRITE.material;
        resetPos = transform.position;
        resetGravity = RB.gravityScale;
        StateMachine.Initialize(IdleState);


    }

    private void Update()
    {
        StateMachine.CurrentBirdState.FrameUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StateMachine.CurrentBirdState.HandleTrigger(other);
    }


    #endregion

    #region Received Event Function
    public void HandleReset()//Receive OnRestart event from EasyModeManager
    {
        SPRITE.material = _resetMaterial;
        StateMachine.ChangeState(ResetState);
    }

    public void HandleFlying() //Receive OnStartFlying event from EasyModeManager
    {
        StateMachine.ChangeState(FlyingState);
    }

    public void HandlePause() //Receive OnGamePause event from EasyModeManager
    {
        StateMachine.ChangeState(PauseState);
    }

    #endregion

    #region IDamageable function 
    public void PlayAnimation(DamageableAnimationStandard animation)
    {

    }

    public void TakeDamage(Vector2 direction, float impactForce, float gravityScale)
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

    public void AddCoin(float coin)
    {
        StateMachine.CurrentBirdState.HandleAddCoin(coin);
    }
    #endregion

    #region PlayerService function
    public override Transform GetTransform()
    {
        return transform;
    }
    #endregion

    #region Control function

    public void Flap()
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.Flap);
        Animator.Play("Flap", -1, 0f);
        PlayDustPS();
    }

    public void RaiseAddPointEvent(float point)
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.TakePoint);
        OnBirdRaisePoint.Raise(point);
    }

    public void RaiseAddCoinEvent(float coin)
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.TakeCoin);
        OnBirdRaiseCoin.Raise(coin);
    }

    public void BirdDead()
    {
        OnBirdRaiseSoundEvent.Raise(SoundType.GameOver);
        SPRITE.material = _whiteMaterial;
        OnBirdDead.Raise();
        OnBirdRaiseImpulseSource.Raise(impulseSource);
        PlayExplosionPS();
        PlayCrashSmokePuffPS();
    }


    public void PlayDustPS()
    {
        dustPS.Play();
    }

    public void PlayExplosionPS()
    {
        _explosionPS.Play();
    }

    public void PlayCrashSmokePuffPS()
    {
        _crashSmokePuffPS.Play();
    }

    public void StopCrashSmokePuffPS()
    {
        _crashSmokePuffPS.Stop();
    }

    #endregion

    #region Animation Trigger
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentBirdState.AnimationTriggerEvent(triggerType);
    }


    public enum AnimationTriggerType
    {
        Example
    }
    #endregion


}
