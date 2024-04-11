using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDmanager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyTextObject;

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] Tower tower;


    public void UpdateMoneyText()
    {
        gameSettings.money = gameSettings.money - tower.towerCost;    
        moneyTextObject.text = $"x {gameSettings.money}";
    }

}
