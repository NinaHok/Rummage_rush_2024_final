using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDmanager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyTextObject;

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] Tower tower;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] EventManagerSO eventManager;

    private void OnEnable()
    {
        eventManager.OnGameOver += DisplayGameOverScreen;

    }

    private void OnDisable()
    {
        eventManager.OnGameOver -= DisplayGameOverScreen;
    }

    public void UpdateMoneyText()
    {
        gameSettings.money = gameSettings.money - tower.towerCost;    
        moneyTextObject.text = $"x {gameSettings.money}";
    }


    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
 