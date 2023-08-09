using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public int maxHp;
    [SerializeField] public int Hp;
    [SerializeField] SwingController swingController;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 10;
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitbox)
    {
        if (hitbox.CompareTag("AttackRange"))
        {
            if (Hp > 0)
            {
                Hp -= swingController.atkDamage;
                StartCoroutine(HitReg());

                if (Hp == 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        IEnumerator HitReg()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }
}
