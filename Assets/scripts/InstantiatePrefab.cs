using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject[] Prefabs;
    public int NumberOfObjects = 14;
    public float SpaceBetweenObjects = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 xAxis = transform.right;
        Vector3 yAxis = transform.up * 0.1f;

        for (var i = 0; i < NumberOfObjects; i++)
        {
            float offset = i * SpaceBetweenObjects;
            Vector3 position = transform.position + yAxis + xAxis * offset;

            int randomIndex = UnityEngine.Random.Range(0, Prefabs.Length);
            GameObject randomPrefab = Prefabs[randomIndex];

            Instantiate(randomPrefab, position, transform.rotation);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
