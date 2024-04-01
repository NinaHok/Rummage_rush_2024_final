using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomebaseHealthBar : MonoBehaviour
{
    // foreground UI element
    [SerializeField] Image healthBarSprite;

    public void UpdateHomebaseHealthBar(float currentHealth, float homebaseHealth)
    {
        healthBarSprite.fillAmount = currentHealth / homebaseHealth;
    }





}
