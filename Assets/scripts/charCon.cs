using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charCon : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float slideForce = 5f;
    public float slideDuration = 0.5f;
    public float sprintMultiplier = 2f;

    private CharacterController cc;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private bool isSprinting = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        float currentMoveSpeed = moveSpeed;

        if (isSprinting)
        {
            currentMoveSpeed *= sprintMultiplier;
        }

        cc.Move(movement * currentMoveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            cc.Move(Vector3.up * jumpForce * Time.deltaTime);
        }

        if (Input.GetButtonDown("Slide") && !isSliding)
        {
            isSliding = true;
            slideTimer = slideDuration;
            cc.Move(Vector3.down * slideForce * Time.deltaTime);
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                isSliding = false;
            }
        }

        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }
    }
}
