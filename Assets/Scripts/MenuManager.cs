using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level_1");
    }

}
