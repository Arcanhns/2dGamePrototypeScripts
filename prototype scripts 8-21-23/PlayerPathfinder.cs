using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathfinder : MonoBehaviour
{
    [SerializeField] GameObject pathFinderPoint;
    [SerializeField] Transform playerPosition;
    [SerializeField] float timer;
    [SerializeField] bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSpawn == true)
        {
            StartCoroutine(PathfinderSpawner());
        }
    }

    IEnumerator PathfinderSpawner()
    {
        canSpawn = false;
        Instantiate(pathFinderPoint, playerPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(timer);
        canSpawn = true;
    }
}
