using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float raycastDistance = 100f;
    public float AlphaValueWhenLookedAt = 0.5f;

    private Color modColor;
    private Color initialColor;
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
            initialColor = hitObject.GetComponent<Renderer>().material.color;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hitObject.GetComponent<Renderer>() != null)
            {
                modColor = hitObject.GetComponent<Renderer>().material.color;
                modColor.a = AlphaValueWhenLookedAt;
                hitObject.GetComponent<Renderer>().material.color= modColor;
            }
        }
        else
        {
            if (hitObject != null)
            {
                var resetColor = hitObject.GetComponent<Renderer>().material.color;
                resetColor.a = 1f;
                hitObject.GetComponent<Renderer>().material.color = resetColor;
            }
            hitObject = null;
        }

        //happens when the button IS pressed
        if (Input.GetButtonDown("Interact"))
        {

            if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
            {
                hitObject = hit.collider.gameObject;
                string tag = hitObject.tag;
                SendTag();
                Destroy(hitObject);
            }
        }
    }

    public void SendTag()
    {
        GameObject otherObject = GameObject.Find("character rigid");
        RandomList otherScript = otherObject.GetComponent<RandomList>();
        otherScript.ReceiveTag(hitObject.tag);
    }
}
