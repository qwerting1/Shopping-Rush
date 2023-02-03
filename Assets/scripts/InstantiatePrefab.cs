using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject Beans;
    public int NumberOfCans = 14;
    public float SpaceBetweenCans = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 xAxis = transform.right;
        Vector3 yAxis = transform.up * 0.1f;
        //Vector3 zAxis = transform.forward;

        for (var i = 0; i < NumberOfCans; i++)
        {
            float offset = i * SpaceBetweenCans;
            Vector3 position = transform.position + yAxis + xAxis * offset;
            Instantiate(Beans, position, transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
