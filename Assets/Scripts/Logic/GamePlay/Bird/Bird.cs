using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour, BirdControls.IBirdActions
{
    private BirdControls controls;

    private Rigidbody2D rb;

    private ParticleSystem birdDamageParticleInstance;


    private Vector3 startPos;

    private Quaternion startRot;

    private bool isBirdAlive = false;


    [SerializeField] private float jumpForce = 100f;

    [SerializeField] private ParticleSystem birdDamageParticle;

    [SerializeField] private AudioClip flapAudioClip;

    public static event Action OnBirdCrashed;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;         
    }

    

  

    

    private void OnEnable()
    {
        controls = new BirdControls();
        controls.Bird.SetCallbacks(this);   // kết nối callback OnJump
        controls.Bird.Enable();
        
        
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Bird.SetCallbacks(null);
            controls.Bird.Disable();
        }
        
        
    }

    private void StartFlying(LogicManager.GameState state)
    {
        isBirdAlive = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Bird starts to fly");
    }

    private void Pause(LogicManager.GameState state)
    {
        isBirdAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Debug.Log("Bird stopped");
    }

    private void ResetPos (LogicManager.GameState state)
    {
        
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot ;
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Reseted bird position");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isBirdAlive )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SoundFXManager.Instance.PlayAudioClip(flapAudioClip, transform, 1f);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBirdAlive)
        {
            isBirdAlive = false;
            OnBirdCrashed?.Invoke();
            BirdCollisionHandle();
            BirdDamageSpawner();
            rb.linearVelocityX = -20;
            Debug.Log("Bird crashed");
        }
    }

    private void BirdCollisionHandle()
    {
        float randNum = UnityEngine.Random.Range(25, 30);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, randNum);
        transform.rotation = Quaternion.Euler(0, 0, randNum);
    } 

    private void BirdDamageSpawner()
    {
        birdDamageParticleInstance = Instantiate(birdDamageParticle, transform.position, Quaternion.identity);
    }

}

