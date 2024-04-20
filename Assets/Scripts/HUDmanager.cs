using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDmanager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyTextObject;

    [SerializeField] GameSettingsSO gameSettings;

    [SerializeField] Tower tower;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject controlsScreen;
    [SerializeField] GameObject randomEventScreen;

    [SerializeField] EventManagerSO eventManager;

    [SerializeField] RandomEvent randomEvent;

    [SerializeField] Button buttonControls;
    [SerializeField] Button buttonPause;
    [SerializeField] Button buttonCloseControls;

    [SerializeField] Button buttonResume;
    [SerializeField] Button buttonExit;

    [SerializeField] Button buttonRestart;



    private void Start()
    {

    }


    private void OnEnable()
    {
        buttonPause.onClick.AddListener(() => {
            eventManager.PauseGame();
            gameSettings.currentGameState = GameStates.paused;
            Time.timeScale = 0f;
        });

        buttonControls.onClick.AddListener(() => {
            ShowControls();
            gameSettings.currentGameState = GameStates.showingControls;
            Time.timeScale = 0f;
        });

        buttonCloseControls.onClick.AddListener(() =>
        {
            HideControls();
            gameSettings.currentGameState = GameStates.inGame;
            Time.timeScale = 1f;
        });

        buttonResume.onClick.AddListener(() =>
        {
            HidePauseScreen();
            gameSettings.currentGameState = GameStates.inGame;
            Time.timeScale = 1f;
        });

        buttonExit.onClick.AddListener(() =>
        {
            gameSettings.previousGameState = gameSettings.currentGameState;
            SceneManager.LoadScene("Main_Menu");
            gameSettings.currentGameState = GameStates.inMainMenu;
        });

        buttonRestart.onClick.AddListener(() => {
            gameSettings.previousGameState = gameSettings.currentGameState;
            SceneManager.LoadScene("Level_1");
            gameSettings.currentGameState = GameStates.inGame;
        });



        eventManager.onGameOver += DisplayGameOverScreen;

        eventManager.onPauseGame += DisplayPauseScreen;
        eventManager.onResumeGame += HidePauseScreen;
        eventManager.onEnemyDestroyed += UpdateMoneyText;
        eventManager.onRandomEvent += ShowRandomEventScreen;

    }

    private void OnDisable()
    {
        eventManager.onGameOver -= DisplayGameOverScreen;

        eventManager.onPauseGame -= DisplayPauseScreen;
        eventManager.onResumeGame -= HidePauseScreen;
        eventManager.onEnemyDestroyed -= UpdateMoneyText;
        eventManager.onRandomEvent -= ShowRandomEventScreen;


        buttonExit.onClick.RemoveListener(() =>
        {
            SceneManager.LoadScene("Main_Menu");
        });

    }

    public void UpdateMoneyText()
    { 
        moneyTextObject.text = $"x {gameSettings.money}";
    }

    public void SubtractTowerCost()
    {
        gameSettings.money -= tower.towerCost;
        moneyTextObject.text = $"x {gameSettings.money}";
    }

    public void DisplayGameOverScreen()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        gameOverScreen.SetActive(true);
        gameSettings.currentGameState = GameStates.gameOver;
    }

    public void DisplayTutorial()
    {
        gameSettings.previousGameState = GameStates.inMainMenu;
        tutorialScreen.SetActive(true);
        gameSettings.currentGameState = GameStates.inTutorial;
    }

    public void HideTutorial()
    {
        tutorialScreen.SetActive(false);

    }

    public void DisplayPauseScreen()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        pauseScreen.SetActive(true);
        gameSettings.currentGameState = GameStates.paused;
    }

    public void HidePauseScreen()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        pauseScreen.SetActive(false);
        gameSettings.currentGameState = GameStates.inGame;
    }

    public void ShowControls()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        controlsScreen.SetActive(true);
    }

    public void HideControls()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        controlsScreen.SetActive(false);
    }

    public void ShowRandomEventScreen()
    {
        gameSettings.previousGameState = gameSettings.currentGameState;
        randomEventScreen.SetActive(true);
        randomEvent.Randomize();
        gameSettings.currentGameState = GameStates.inRandomEvent;
    }

    public void HideRandomEventScreen()
    {
        randomEventScreen.SetActive(false);
    }
}


 