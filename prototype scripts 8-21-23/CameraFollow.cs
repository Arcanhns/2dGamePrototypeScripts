using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] public Transform playerPosition;
    [SerializeField] public Transform bossPosition;
    [SerializeField] public bool followPlayer = true;


    // Update is called once per frame
    void Update()
    {

        if (followPlayer == true)
        {
            Vector3 newPos = new Vector3(playerPosition.position.x, playerPosition.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 newPos = new Vector3(bossPosition.position.x, bossPosition.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        
    }
}
