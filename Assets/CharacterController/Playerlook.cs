using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlook : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerBody; //reference our player

    public float mouseSensitivity = 100f; //choose how sensitive the mouse will be
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks our cursor in the middle of the screen
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX;
        playerBody.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
