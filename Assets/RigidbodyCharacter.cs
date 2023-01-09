using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float Speed = 5f;
    public float JumpForce = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private Rigidbody body;
    private bool isGrounded = true;
    private Transform groundChecker;
    Vector3 moveDirection;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        if (isGrounded)
        {
            body.drag = 8f;
        } else
        {
            body.drag = 2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        { 
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            body.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        
        var moveSpeed = Speed;
        if (!isGrounded) {
            moveSpeed = Speed / 6;
        }

        body.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }

}