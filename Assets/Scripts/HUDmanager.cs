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

    [SerializeField] EventManagerSO eventManager;

    [SerializeField] Button buttonControls;
    [SerializeField] Button buttonPause;
    [SerializeField] Button buttonCloseControls;

    [SerializeField] Button buttonResume;
    [SerializeField] Button buttonExit;



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
            SceneManager.LoadScene("Main_Menu");
        });




        eventManager.onGameOver += DisplayGameOverScreen;

        eventManager.onPauseGame += DisplayPauseScreen;
        eventManager.onResumeGame += HidePauseScreen;

    }

    private void OnDisable()
    {
        eventManager.onGameOver -= DisplayGameOverScreen;

        eventManager.onPauseGame -= DisplayPauseScreen;
        eventManager.onResumeGame -= HidePauseScreen;



        buttonExit.onClick.RemoveListener(() =>
        {
            SceneManager.LoadSceneAsync("Main_Menu");
        });

    }

    public void UpdateMoneyText()
    {
        gameSettings.money = gameSettings.money - tower.towerCost;    
        moneyTextObject.text = $"x {gameSettings.money}";
    }


    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameSettings.currentGameState = GameStates.gameOver;
    }

    public void DisplayTutorial()
    {
        tutorialScreen.SetActive(true);
        gameSettings.currentGameState = GameStates.inTutorial;
    }

    public void HideTutorial()
    {
        tutorialScreen.SetActive(false);
        gameSettings.currentGameState = GameStates.inGame;

    }

    public void DisplayPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void ShowControls()
    {
        controlsScreen.SetActive(true);
    }

    public void HideControls()
    {
        controlsScreen.SetActive(false);
    }

}


 