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
    public event Action onRandomEvent;
    public event Action onWin;

    public event Action onBananaPeel;
    public event Action onCardboardBox;
    public event Action onCrushedCan;
    public event Action onLavalamp;
    public event Action onMoldyBrownie;
    public event Action onPlasticKnife;


    public void RandomEvent()
    {
        onRandomEvent?.Invoke();
    }

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

    public void Win()
    {
        onWin?.Invoke();
    }

    //Random Item Events

    public void BananaPeel()
    {
        onBananaPeel?.Invoke();
    }

    public void CardboardBox()
    {
        onCardboardBox?.Invoke();
    }

    public void CrushedCan()
    {
        onCrushedCan?.Invoke();
    }

    public void Lavalamp()
    {
        onLavalamp?.Invoke();
    }

    public void MoldyBrownie()
    {
        onMoldyBrownie?.Invoke();
    }

    public void PlasticKnife()
    {
        onPlasticKnife?.Invoke();
    }

}
