using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    [SerializeField] float spawnTimer = 10f;
    [SerializeField] public bool pickedUp = false;
    [SerializeField] GameObject healthPack;
    [SerializeField] PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(healthPack);
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp == true)
        {
            StartCoroutine(HealthPackRespawn());
            pickedUp = false;
        }
    }

    IEnumerator HealthPackRespawn()
    {
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(healthPack);
    }
}
