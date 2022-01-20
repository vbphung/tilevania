using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void SetDefaultVolume()
    {
        FindObjectOfType<OptionController>().SetDefault();
    }

    public void SaveVolumeAndExit()
    {
        FindObjectOfType<OptionController>().Save();
        LoadMenu();
    }
}
