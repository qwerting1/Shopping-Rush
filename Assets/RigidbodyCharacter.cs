//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class controllerrigid : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    public float speed = 10f;
//    public Rigidbody controller;
//    void Update()
//    {
//        // Get input for movement
//        float moveHorizontal = Input.GetAxis("Horizontal");
//        float moveVertical = Input.GetAxis("Vertical");

//        // Calculate movement vector
//        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//        // Apply movement to the rigidbody
//        GetComponent<Rigidbody>().AddForce(movement * speed);
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    public Transform playerBody; //reference our player
    Vector3 moveDirection;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        // _inputs = Vector3.zero;
        // _inputs.x = Input.GetAxis("Horizontal");
        // _inputs.z = Input.GetAxis("Vertical");
        //if (_inputs != Vector3.zero)
        //{
        //    // _body.MoveRotation
        //    // transform.forward = _inputs;
        //    // transform.right
        //    // _body.AddForce((transform.forward / 1000) / Time.deltaTime, ForceMode.Impulse);
        //}

        //if (Input.GetButtonDown("Jump") && _isGrounded)
        //{
        //    _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        //}
        //if (Input.GetButtonDown("Dash"))
        //{
        //    Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
        //    _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        //}
    }


    //void FixedUpdate()
    //{
    //    _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    //}

    void FixedUpdate()
    {
        // var targetDir = sphere.transform.position - transform.position;
        //var targetDir = transform.position;
        //var forward = transform.forward;
        //var localTarget = transform.InverseTransformPoint(transform.position);

        //var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        //var eulerAngleVelocity = new Vector3(0, angle, 0);
        //var deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        //_body.MoveRotation(transform.rotation * deltaRotation);
        _body.AddForce(moveDirection.normalized * 6f * 10f, ForceMode.Acceleration);
        // _body.AddForce(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

}