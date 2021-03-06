﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    public float jumpSpeed;

    private Rigidbody _rb;

    private Collider col;

    private bool pressedJump;

    private Vector3 size;

    public AudioSource CoinSound;

    //camera distance on z
    public float CameraDistZ = 6;

    //value that you fell on bottom
    private float minY = -1.6f;

	// Use this for initialization
	void Start ()
	{
	    _rb = GetComponent<Rigidbody>();
	    col = GetComponent<Collider>();

        //get size of the player
	    size = col.bounds.size;

        //set the camera position initially
        CameraFollowPlayer();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		WalkHandler();
        JumpHandler();
	    CameraFollowPlayer();
	    FallHandler();
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

        //CHECK IF THERE'S ANY MOVEMENT (FOR MAKING THE PLAYER FACE THE MOVEMENT DIRECTION)
        if (hAxis != 0 || vAxis != 0)
        {
            Vector3 Direction = new Vector3(hAxis,0,vAxis);

            //option one modify the transform though its not a good idea to change the transfrom directly if it has a rigidbody unless it's kinematic 
           // transform.forward = Direction;

        //so we use
        _rb.rotation = Quaternion.LookRotation(Direction);

        }
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
        //Debug.DrawRay(corner1, -Vector3.up * 0.1f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        //Debug.DrawRay(corner2, -Vector3.up * 0.1f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        //Debug.DrawRay(corner3, -Vector3.up * 0.1f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);
        //Debug.DrawRay(corner4, -Vector3.up * 0.1f);
        bool midFaceCheck = Physics.Raycast(midFace, -Vector3.up, 0.01f);
        //Debug.DrawRay(midFace, -Vector3.up * 0.1f);

        return (grounded1 || grounded2 || grounded3 || grounded4 || midFaceCheck);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
           // print("collected a coin");
            GameManager.Instance.IncreaseScore(1);

            // play coin sound
            CoinSound.Play();
            //destroy coin game object
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            //print("you've run into an enemy");
            //Game over
            GameManager.Instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            //print("you made it");
            //Increse level
            GameManager.Instance.IncreseLevel();
        }

    }

    void CameraFollowPlayer()
    {
        //grab the camera
        Vector3 cameraPosition = Camera.main.transform.position;

        //modify it's position according to Camera Distace on Z
        cameraPosition.z = transform.position.z - CameraDistZ;

        //set camera position
        Camera.main.transform.position = cameraPosition;


    }

    //check if the player fell
    private void FallHandler()
    {
        if (transform.position.y <= minY)
        {
            GameManager.Instance.GameOver();
        }
    }
}
