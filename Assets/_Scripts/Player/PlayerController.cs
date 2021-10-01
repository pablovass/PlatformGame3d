using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour

{
    [SerializeField]private float jumpForce = 5;
    [SerializeField] private float gravityMultiplier;
    private bool isOnGround = true; 
    //public bool gameOver = false; always is falses
    private bool _gameOver;
    public bool GameOver { get=>_gameOver; }
    
    private Rigidbody playerRb;

    private Animator _animator;
    

    private bool IsMovingJump = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;
        
        _animator = GetComponent<Animator>();
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !GameOver)
        {
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //F=m*a
            IsMovingJump = !IsMovingJump;
            _animator.SetTrigger("Jump_trig");
            isOnGround = false;
        } 
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) //SOLO SALTA SI esta en el suelo
        {
            isOnGround = true;    
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;
            Debug.Log("Game Over");
            _animator.SetBool("Death_b",true);
            _animator.SetInteger("DeathType_int",1);
        }
        
    }
}
