using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
            Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetGameMusicVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
