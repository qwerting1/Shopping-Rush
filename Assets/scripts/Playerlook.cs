using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlook : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform PlayerBody;
    public float MouseSensitivity = 1.5f;
    public float LookUpLimit = -55f;
    public float LookDownLimit = 60f;

    float yRotation;
    float xRotation = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //locks our cursor in the middle of the screen
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * MouseSensitivity;
        xRotation -= mouseY * MouseSensitivity;
    }

    private void FixedUpdate()
    {
        PlayerBody.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        xRotation = Mathf.Clamp(xRotation, LookUpLimit, LookDownLimit);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
