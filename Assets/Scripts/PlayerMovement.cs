using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 3f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    public GameObject playerObject;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed * 0);
        


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

    }
    
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        playerObject.GetComponent<Animator>().Play("Jump");
    }


   
    
    
    bool IsGrounded()
    {
       return Physics.CheckSphere(groundCheck.position, .1f, ground);
    } 
    
}
