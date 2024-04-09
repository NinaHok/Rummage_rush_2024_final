using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "Managers/Game Settings")]
public class GameSettingsSO : ScriptableObject
{

    public int money = 50;

    public void UpdateMoney(int towerCost,int moneyLeft)
    {
        moneyLeft = money - towerCost;
        moneyTextObject.text = $"x {moneyLeft}";
    }


}
