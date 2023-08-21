using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject batBoss;
    [SerializeField] PlayerController playerController;
    [SerializeField] RotationPoint rotationPoint;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canSpawn == true)
        {
            Instantiate(batBoss);
            canSpawn = false;

            StartCoroutine(DisablePlayerControls());
            StartCoroutine(CameraLock());
        }
    }

    IEnumerator CameraLock()
    {
        cameraFollow.followPlayer = false;
        yield return new WaitForSeconds(2.5f);
        cameraFollow.followPlayer = true;
    }

    IEnumerator DisablePlayerControls()
    {
        playerController.playerCanMove = false;
        rotationPoint.playerCanShoot = false;
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;
        rotationPoint.playerCanShoot = true;
    }

}
