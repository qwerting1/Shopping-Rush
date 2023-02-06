using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject[] Prefabs;
    public int NumberOfObjects = 36;
    public float SpaceBetweenObjects = 0.79f;
    public Quaternion RotationOffset;
    public float HeightOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 xAxis = transform.right;
        Vector3 yAxis = transform.up * HeightOffset;

        for (var i = 0; i < NumberOfObjects; i++)
        {
            float offset = i * SpaceBetweenObjects;
            Vector3 position = transform.position + yAxis + xAxis * offset;
            Quaternion rotation = transform.rotation * RotationOffset;

            int randomIndex = UnityEngine.Random.Range(0, Prefabs.Length);
            GameObject randomPrefab = Prefabs[randomIndex];

            Instantiate(randomPrefab, position, rotation);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
