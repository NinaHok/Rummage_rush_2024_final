using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "Managers/Game Settings")]
public class GameSettingsSO : ScriptableObject
{

    public float money = 50f;

    public float damageDealt = 0f;

    [SerializeField] public GameStates currentGameState;
    [SerializeField] public GameStates previousGameState;


    private void Awake()
    {
        currentGameState = GameStates.inMainMenu;
        previousGameState = currentGameState;

    }

    public void ResetMoney()
    {
        money = 50f;
    }

    public void ResetDamageDealt()
    {
        damageDealt = 0f;
    }

}
