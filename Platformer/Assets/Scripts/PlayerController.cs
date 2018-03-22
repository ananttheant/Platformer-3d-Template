﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    public float jumpSpeed;

    private Rigidbody _rb;

    private Collider col;

    private bool pressedJump;

    private Vector3 size;

    public AudioSource CoinSound;

	// Use this for initialization
	void Start ()
	{
	    _rb = GetComponent<Rigidbody>();
	    col = GetComponent<Collider>();

        //get size of the play
	    size = col.bounds.size;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		WalkHandler();
        JumpHandler();

	}

    void WalkHandler()
    {
        // Input on x (horizontal)
        float hAxis = Input.GetAxis("Horizontal");

        //Input on z (verticle)
        float vAxis = Input.GetAxis("Vertical");

        //movement vector
        Vector3 Movement = new Vector3(walkSpeed * Time.deltaTime * hAxis, 0, walkSpeed * Time.deltaTime * vAxis);

        //new position
        Vector3 NewPos = transform.position + Movement;

        _rb.MovePosition(NewPos);
    }

    void JumpHandler()
    {
        //input on the y axis
        float jAxis = Input.GetAxis("Jump");

        //if key pressed
        if (jAxis > 0)
        {
            bool isGrounded = checkGrounded();
            
            //make sure we are not alredy jumping
            if (!pressedJump && isGrounded)
            {
                pressedJump = true;
          
                // jumping vector
                Vector3 JumpVector = new Vector3(0, jumpSpeed * jAxis, 0);


                _rb.AddForce(JumpVector, ForceMode.VelocityChange);
            }
                
        }
        else
        {
            pressedJump = false;
        }

    }

    private bool checkGrounded()
    {
        // location of all 4 corners
        Vector3 corner1= transform.position + new Vector3(size.x/2, -size.y/2 + 0.01f,size.z/2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y /2 + 0.01f, size.z / 2);

        Vector3 corner3= transform.position + new Vector3(size.x/2, -size.y/2 + 0.01f, -size.z/2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        Vector3 midFace = transform.position + new Vector3(0, -size.y / 2 + 0.01f, 0);

        //raycast (check if grounded)
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);
        bool midFaceCheck = Physics.Raycast(midFace, -Vector3.up, 0.01f);

        return(grounded1 || grounded2 || grounded3 || grounded4 || midFaceCheck);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            print("collected a coin");

            // play coin sound
            CoinSound.Play();
            //destroy coin game object
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            print("you've run into an enemy");
        }
        else if (other.CompareTag("Goal"))
        {
            print("you made it");
        }
    }
}
