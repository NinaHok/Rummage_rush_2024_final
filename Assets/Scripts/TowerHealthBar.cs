using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    // foreground UI element
    [SerializeField] Image towerHealthBarSprite;

    public void UpdateHealthBar(float currentTowerHealth, float towerHealth)
    {
        towerHealthBarSprite.fillAmount = currentTowerHealth / towerHealth;
    }





}
