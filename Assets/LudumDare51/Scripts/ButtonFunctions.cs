using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
