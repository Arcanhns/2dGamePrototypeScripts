using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    [SerializeField] public int atkDamage = 2;
    [SerializeField] GameObject attackRange;
    [SerializeField] bool readyToAttack = true;

    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButtonDown(0) && readyToAttack == true)
        {
            StartCoroutine(SwingAttack());
        }
    }

    private IEnumerator SwingAttack()
    {
        attackRange.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackRange.SetActive(false);
        readyToAttack = false;
        yield return new WaitForSeconds(0.1f);
        readyToAttack = true;
    }
}
