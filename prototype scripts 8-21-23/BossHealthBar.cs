using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] GameObject bossHealthBar;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        bossHealthBar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        healthSlider.value = currentValue / maxValue;

        if (currentValue == 0)
        {
            bossHealthBar.SetActive(false);
        }
    }
}
