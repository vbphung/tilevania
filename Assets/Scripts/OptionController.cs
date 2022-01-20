using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.5f;

    MusicPlayer musicPlayer = null;

    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        volumeSlider.value = PlayerPrefsController.GetGameMusicVolume();
    }

    void Update()
    {
        if (musicPlayer)
            musicPlayer.SetVolume(volumeSlider.value);
    }

    public void Save()
    {
        PlayerPrefsController.SetGameMusicVolume(volumeSlider.value);
    }

    public void SetDefault()
    {
        volumeSlider.value = defaultVolume;
    }
}