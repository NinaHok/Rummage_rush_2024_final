using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameSettingsSO gameSettings;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Level_1")
        {
            gameSettings.ResetMoney();
        }
    }
}
