using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update


    public CharacterController controller;

    public float speed = 12f;
    public float speedMod=2;

    public float gravity = -9.81f; //normal earth accel

    Vector3 velocity;

    public Transform groundCheck; //reference our ground check
    public float groundDistance = 0.4f;//size of sphere
    public LayerMask groundMask; //check layers
    bool isGrounded; //check if it is grounded or not

    public float jumpHeight = 3f;

    public bool extraJump = false;
    private void Start()
    {
        controller.detectCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //check if player is grounded

        if(isGrounded && velocity.y<0) //if our player is grounded and with velocity less than 0
        {
            velocity.y = -2f; // we reset player velocity
        }

        float x = Input.GetAxis("Horizontal"); // horizontal  axis. A and D button
        float z = Input.GetAxis("Vertical");// Vertical axis W and S button
        //if (input.getkey("x"))
        //{
        //    vector3 move = transform.right * x + transform.forward * z * speedmod;
        //    controller.move(move * speed * time.deltatime);
        //}
        //else
        //{
        //    vector3 move = transform.right * x + transform.forward * z;
        //    controller.move(move * speed * time.deltatime);
        //}
        // put all our movement into a vector3

        //move our character controller

        velocity.y += gravity * Time.deltaTime; //normalize accel with time 

        controller.Move(velocity * Time.deltaTime);


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
            extraJump = true;
        }
        else if(Input.GetButtonDown("Jump") && extraJump == true)
        {
             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
            extraJump = false; 
        }

    }

}





