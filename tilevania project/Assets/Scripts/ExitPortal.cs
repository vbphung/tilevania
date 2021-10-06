using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        GameSession currentGS = FindObjectOfType<GameSession>();
        if (currentGS)
            Destroy(currentGS.gameObject);

        ScenePersist currentSP = FindObjectOfType<ScenePersist>();
        if (currentSP)
            Destroy(currentSP.gameObject);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
