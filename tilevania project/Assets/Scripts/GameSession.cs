using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Dialogue.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 1;
    [SerializeField] Text livesText;
    [SerializeField] DialogueUI dialogueUI = null;

    void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
            Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
            StartCoroutine(TakeLife());
        else ResetGame();
    }

    private IEnumerator TakeLife()
    {
        --playerLives;
        livesText.text = playerLives.ToString();

        yield return new WaitForSeconds(3);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        dialogueUI.Reset();
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);

        ScenePersist currentSP = FindObjectOfType<ScenePersist>();
        if (currentSP)
            Destroy(currentSP.gameObject);
        Destroy(gameObject);
    }

    public void RecieveLives(int givenLives)
    {
        playerLives += givenLives;
        livesText.text = playerLives.ToString();
    }
}
