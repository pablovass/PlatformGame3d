using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour

{
    private const string SPEED_F = "Speed_f";
    [SerializeField]private float jumpForce = 5;
    [SerializeField] private float gravityMultiplier=1;
    private bool isOnGround = true; 
    //public bool gameOver = false; always is falses
    private bool _gameOver;
    public bool GameOver { get=>_gameOver; }
    
    private Rigidbody playerRb;

    private Animator _animator;
    private float SpeedMultiplier;
    private bool IsMovingJump = true;
    public ParticleSystem explosion;
    public ParticleSystem dirt;
    public AudioClip jumpSound, crashSound;

    private AudioSource _audioSource;
   
    // Start is called before the first frame update
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = gravityMultiplier * new Vector3(0, -9.81f, 0);
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F,1);
        _audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        SpeedMultiplier += Time.deltaTime/10;
        
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !GameOver)
        {
            
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //F=m*a
            IsMovingJump = !IsMovingJump;
            _animator.SetTrigger("Jump_trig");
            _audioSource.PlayOneShot(jumpSound,1);
            isOnGround = false;
        } 
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) //SOLO SALTA SI esta en el suelo
        {
            if (!GameOver)
            {
                isOnGround = true;
                dirt.Play();
            }
                
        } else if (other.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;
            Debug.Log("Game Over");
            explosion.Play();
            _animator.SetBool("Death_b",true);
            _animator.SetInteger("DeathType_int",1);
            dirt.Stop();
            _audioSource.PlayOneShot(crashSound,Random.Range(1,3));
            Invoke("RestartGame",1.0f);
            
        }
        
    }

    void RestartGame()
    {
        SpeedMultiplier = 1;
       SceneManager.LoadSceneAsync("Prototype 3",LoadSceneMode.Single);
        
    }
} 
