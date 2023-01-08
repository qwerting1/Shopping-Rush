using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public CharacterController controller;

    public float walk = 12f;
    public float sprint = 24f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    float slideEndTime = 0f;
    public float slideDuration = 0.3f;
    public float slideSpeed = 50f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float time = Time.time;
        // check if user has pressed slide button and we are not sliding
        if (isGrounded && Input.GetButtonDown("Slide") && time > slideEndTime)
        {
            slideEndTime = time + slideDuration;
        }

        if (time < slideEndTime) 
        {
            Vector3 move = transform.forward;
            controller.Move(move * slideSpeed * Time.deltaTime);
        }
        else
        {
            HandleMovement();
        }

    }

    public void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = walk;
        if (Input.GetButton("Sprint"))
        {
            speed = sprint;
            Debug.Log("sprinting");
        }


        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }


}

