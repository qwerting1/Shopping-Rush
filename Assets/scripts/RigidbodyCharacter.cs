using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float JumpForce = 500f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    public float SprintSpeed = 160f;
    public float WalkSpeed = 80f;
    public float SlideSpeed = 8000f;

    private float Speed;
    private Rigidbody body;
    private bool isGrounded = true;
    private Transform groundChecker;
    Vector3 moveDirection;

    void Start()
    {
        Speed = WalkSpeed;
        body = GetComponent<Rigidbody>();
        groundChecker = GameObject.Find("PlayerFeet").transform;
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
        }
        else
        {
            body.drag = 2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            body.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }

        if (Input.GetButtonDown("Sprint") && isGrounded)
        {
            Speed = SprintSpeed;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            Speed = WalkSpeed;
        }

        if (Input.GetButtonDown("Slide") && isGrounded)
        {
            var slideDirection = transform.forward;
            body.AddForce(slideDirection.normalized * SlideSpeed, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {

        var moveSpeed = Speed;
        if (!isGrounded)
        {
            moveSpeed = Speed / 6;
        }
        body.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }
}