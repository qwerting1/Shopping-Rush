using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] public GameObject item;
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="Player")
        {
            if(Input.GetKeyDown("space"))
            {
                print("space key was pressed");
            }
            Debug.Log("player is in");
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("player is out");
        }
    }
}
