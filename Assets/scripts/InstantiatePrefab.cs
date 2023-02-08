using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject[] Prefabs;
    public int NumberOfObjects = 36;
    [Tooltip("Space between objects")]
    public float XOffset = 0.79f;
    public float YOffset = 0.1f;
    public float ZOffset = 0f;
    public Vector3 RotationOffset;


    // Start is called before the first frame update
    void Start()
    {
        Quaternion RotationOffsetQ = Quaternion.Euler(RotationOffset); //convert vector3 to quaternion
        Vector3 xAxis = transform.right;
        Vector3 yAxis = transform.up * YOffset;
        Vector3 zAxis = transform.forward * ZOffset;

        for (var i = 0; i < NumberOfObjects; i++)
        {
            float offset = i * XOffset;
            Vector4 position = transform.position + yAxis + zAxis + xAxis * offset;
            Quaternion rotation = transform.rotation * RotationOffsetQ;

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
