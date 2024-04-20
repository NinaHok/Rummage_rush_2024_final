using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] HUDmanager hudManager;
    [SerializeField] EventManagerSO eventManager;
    [SerializeField] RandomEvent randomEvent;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TowerSpawner towerSpawner;


    [SerializeField] Homebase homebase;

    [SerializeField] float randomEventTimer;
    [SerializeField] float randomEventDuration = 10f;

    [SerializeField] Tower towerDefault;
    [SerializeField] Tower towerFast;
    [SerializeField] Tower towerHeavy;

    [SerializeField] Enemy enemyDefault;
    [SerializeField] Enemy enemyFast;
    [SerializeField] Enemy enemyHeavy;

    private void Awake()
    {

        if (gameSettings.previousGameState == GameStates.inMainMenu)
        {
            
            hudManager.DisplayTutorial();
            gameSettings.currentGameState = GameStates.inTutorial;
            Time.timeScale = 0f;
        }

        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            gameSettings.currentGameState = GameStates.inGame;
            Time.timeScale = 1f;
            gameSettings.ResetMoney();
            gameSettings.ResetDamageDealt();
            gameSettings.enemiesSpawned = 7;
            gameSettings.enemiesDestroyed = 0;

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
            if (gameSettings.damageDealt == 30f)
            {
                randomEventTimer = 0f;
                gameSettings.previousGameState = GameStates.inGame;
                gameSettings.currentGameState = GameStates.inRandomEvent;
                Time.timeScale = 0f;
                eventManager.RandomEvent();
                gameSettings.damageDealt = 0f;
            }
        }

        if (gameSettings.currentGameState == GameStates.inRandomEvent)
        {
            if (Input.anyKeyDown)
            {
                randomEventTimer = 0f;
                gameSettings.previousGameState = gameSettings.currentGameState;
                hudManager.HideRandomEventScreen();
                gameSettings.currentGameState = GameStates.inGame;
                Time.timeScale = 1f;
            }
        }

        if (gameSettings.previousGameState == GameStates.inRandomEvent && 
            gameSettings.currentGameState == GameStates.inGame && 
            randomEvent.item != null &&
            randomEvent.itemName != null )

        {
           
            randomEventTimer += Time.deltaTime;

            if (randomEventTimer < randomEventDuration)
            {

                if (randomEvent.itemName == "Banana peel")
                {
                    foreach (Enemy enemy in enemySpawner.enemies)
                    enemy.speed = 1f;
                }

                else if (randomEvent.itemName == "Cardboard box")
                {
                    homebase.damageTakingDelay = 10f;
                }

                else if (randomEvent.itemName == "Crushed can")
                { 
                    foreach(Tower tower in towerSpawner.towers)
                    tower.firingDelay = 0.3f;
                }

                else if (randomEvent.itemName == "Lavalamp")
                {
                    foreach (Tower tower in towerSpawner.towers)
                        tower.firingDelay = 10f;
                }

                else if (randomEvent.itemName == "Moldy brownie")
                {;
                    foreach (Tower tower in towerSpawner.towers)
                        tower.firingDelay = 10f;
                }

                else if (randomEvent.itemName == "Plastic knife")
                {
                    foreach (Enemy enemy in enemySpawner.enemies)
                        enemy.maxHealth = 5f;
                }

            }
             else if (randomEventTimer >= randomEventDuration)
            {
                randomEvent.item = null;
                randomEvent.itemName = null;

                foreach (Enemy enemy in enemySpawner.enemies)
                    enemy.speed = enemy.defaultSpeed;

                foreach (Enemy enemy in enemySpawner.enemies)
                    enemy.maxHealth = enemy.defaultHealth;

                foreach (Tower tower in towerSpawner.towers)
                    tower.firingDelay = tower.defaultFiringDelay;

                homebase.damageTakingDelay = homebase.defaultDamageTakingDelay;
            }


        }
        if (gameSettings.currentGameState == GameStates.inGame && 
            gameSettings.enemiesDestroyed == gameSettings.enemiesSpawned)
        {
            eventManager.Win();
            Time.timeScale = 0f;
        }




               
                
                 
    }
}

    

