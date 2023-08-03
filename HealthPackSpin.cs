using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;

        float zRotation = currentRotation.z + (rotationSpeed * Time.deltaTime);

        currentRotation.z = zRotation;

        transform.eulerAngles = currentRotation;
    }
}
