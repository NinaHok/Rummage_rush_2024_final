using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDmanager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyTextObject;

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] Tower tower;


    public void UpdateMoneyText(float moneyLeft)
    {
        moneyLeft = gameSettings.money - tower.towerCost;
        moneyTextObject.text = $"x peepeepoopoo";
    }

}
