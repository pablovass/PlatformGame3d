using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour

{
    [SerializeField]private float jumpForce = 5;
    [SerializeField] private float gravityMultiplier;
    private bool isOnGround = true;
    
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMultiplier;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //F=m*a
            isOnGround = false;
        } 
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) //SOLO SALTA SI esta en el suelo
        {
            isOnGround = true;    
        }
        
    }
}
