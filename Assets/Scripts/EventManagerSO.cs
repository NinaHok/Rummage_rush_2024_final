using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="EventManager", menuName = "Managers/EventManager")]
public class EventManagerSO : ScriptableObject
{

    public event Action onGameOver;

    public event Action onPauseGame;
    public event Action onResumeGame;
    public event Action onEnemyDestroyed;

    public void EnemyDestroyed()
    { onEnemyDestroyed?.Invoke(); }
    public void PauseGame()
    {
        onPauseGame?.Invoke();
    }

    public void ResumeGame()
    {
        onResumeGame?.Invoke();
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }


}
