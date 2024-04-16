using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "Managers/Game Settings")]
public class GameSettingsSO : ScriptableObject
{

    public float money = 50f;

    [SerializeField] Tower tower;

    public void ResetMoney()
    {
        money = 50f;
    }


}
