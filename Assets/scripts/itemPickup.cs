using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    public Material NormalMaterial;
    public Material InteractMaterial;

    // Start is called before the first frame update
    void Start()
    {
        item.GetComponent<MeshRenderer>().material = NormalMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] public GameObject item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            item.GetComponent<MeshRenderer>().material = InteractMaterial;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButton("Interact"))
            {
                Destroy(item);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            item.GetComponent<MeshRenderer>().material = NormalMaterial;
        }
    }
}
