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


    [SerializeField] private float jumpForce = 100f;

    [SerializeField] private ParticleSystem birdDamageParticle;

    [SerializeField] private AudioClip flapAudioClip;

    

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
        controls.Bird.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (controls != null)
            controls.Bird.SetCallbacks(null);          
    }

    public void StartFlying()
    {
        Debug.Log("Main bird received OnStartFlying event");
        ResetPos();
        rb.bodyType = RigidbodyType2D.Dynamic;
        controls.Bird.Enable();

    }

    private void ResetPos ()
    {
        
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot ;
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Reseted bird position");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SoundFXManager.Instance.PlayAudioClip(flapAudioClip, transform, 1f);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        controls.Bird.Disable();
        BirdCollisionHandle();
        BirdDamageSpawner();
        rb.linearVelocityX = -20;
        Debug.Log("Bird crashed");
    }

    private void BirdCollisionHandle()
    {
        float randNum = UnityEngine.Random.Range(25, 30);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, randNum);
        transform.rotation = Quaternion.Euler(0, 0, randNum);
    } 

    private void BirdDamageSpawner()
    {
        if(birdDamageParticleInstance!= null)
            birdDamageParticleInstance = Instantiate(birdDamageParticle, transform.position, Quaternion.identity);
    }

}

