using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
 

    public void LoadScene(SceneAsset sceneToBeLoaded)
    {
        SceneManager.LoadScene(sceneToBeLoaded.name);
    }


}
