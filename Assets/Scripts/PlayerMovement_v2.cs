using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v2 : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float LRmovementSpeed = 5f;
    [SerializeField] float VHmovementSpeed = 0f;                     // Vorne / Hinten Movement Speed
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    public GameObject playerObject;


    

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();     // Get the Rigidbody component from the player GameObject
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        // Preserve the vertical velocity
        float verticalVelocity = rb.velocity.y;

        // Reset horizontal velocity when neither left nor right key is pressed
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.9f, verticalVelocity, VHmovementSpeed);
        }

       // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, VHmovementSpeed);
        
        if(Input.GetKey("left"))
        {
            GoLeft();
        }

        if(Input.GetKey("right"))
        {
            GoRight();
        }


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

    }
    

    void GoLeft()
    {
        rb.velocity = new Vector3(LRmovementSpeed * -1, rb.velocity.y, rb.velocity.z);
        /*
        if (IsGrounded())
        {
            playerObject.GetComponent<Animator>().Play("Right Strafe");     //Adds animation for moving right
        }
        */
        
    }

    void GoRight()
    {
        rb.velocity = new Vector3(LRmovementSpeed, rb.velocity.y, rb.velocity.z);
        /*
        if (IsGrounded())
        {
            playerObject.GetComponent<Animator>().Play("Left Strafe");        //Adds animation for moving right
        }
        */
        
    }



    void Jump()
    {
        FindObjectOfType<SoundManager>().PlaySound("JumpSFX");
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        playerObject.GetComponent<Animator>().Play("Jump");
    }


   
    
    
    bool IsGrounded()
    {
       return Physics.CheckSphere(groundCheck.position, .1f, ground);
    } 
    
}
