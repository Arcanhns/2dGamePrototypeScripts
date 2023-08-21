using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathfinderPoint : MonoBehaviour
{
    [SerializeField] float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
