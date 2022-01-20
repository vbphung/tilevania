using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int defaultSceneIndex;

    void Awake()
    {
        if (FindObjectsOfType<ScenePersist>().Length > 1)
            Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        defaultSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != defaultSceneIndex)
            Destroy(gameObject);
    }
}
