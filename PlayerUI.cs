using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Text healthText;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Image dashbuttonColor;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = playerHealth.playerHp.ToString() + " HP";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealth.playerHp.ToString() + " HP";

        if (playerMovement.isDashReady == true)
        {
            dashbuttonColor.color = Color.green;
        }
        else
        {
            dashbuttonColor.color = Color.red;
        }
    }
}
