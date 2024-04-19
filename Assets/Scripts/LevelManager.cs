using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] HUDmanager hudManager;
    [SerializeField] EventManagerSO eventManager;



    private void Awake()
    {

        if (gameSettings.previousGameState == GameStates.inMainMenu)
        {
            
            hudManager.DisplayTutorial();

            Time.timeScale = 0f;
        }

        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            gameSettings.ResetMoney();
            gameSettings.ResetDamageDealt();

        }
        
    }

    private void Update()
    {
        if(gameSettings.currentGameState== GameStates.inTutorial)
        {
            if (Input.anyKeyDown)
            {
                gameSettings.previousGameState = gameSettings.currentGameState;
                hudManager.HideTutorial();
                eventManager.ResumeGame();
                gameSettings.currentGameState = GameStates.inGame;
                Time.timeScale = 1f;
            }
        }

        else if(gameSettings.currentGameState == GameStates.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                eventManager.PauseGame();
                gameSettings.currentGameState = GameStates.paused;
                Time.timeScale = 0f;
            }
        }

        else if (gameSettings.currentGameState == GameStates.paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                eventManager.ResumeGame();
                gameSettings.currentGameState = GameStates.inGame;
                Time.timeScale = 1f;
            }
        }

        else if (gameSettings.currentGameState == GameStates.showingControls)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hudManager.HideControls();
                eventManager.ResumeGame();
                gameSettings.currentGameState = GameStates.inGame;
                Time.timeScale = 1f;
            }
        }

        if (gameSettings.currentGameState == GameStates.inGame)
        {
            if (gameSettings.damageDealt == 15f)
            {
                gameSettings.currentGameState = GameStates.inRandomEvent;
                eventManager.RandomEvent();
            }
        }



    }
}
