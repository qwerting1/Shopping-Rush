using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float raycastDistance = 5f;

    private GameObject hitObject;
    private LayerMask layerMask;

    void Start()
    {
        layerMask = 1 << 8;
    }

    void Update()
    {
        //happens when the button is NOT pressed
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
        {
            hitObject = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            hitObject.SendMessage("Receive", hitObject.GetInstanceID(), SendMessageOptions.RequireReceiver);
        }
    }
}
