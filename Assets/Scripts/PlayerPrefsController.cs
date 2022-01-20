using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string GAME_MUSIC_VOLUME = "game music volume";

    public static void SetGameMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(GAME_MUSIC_VOLUME, volume);
    }

    public static float GetGameMusicVolume()
    {
        return PlayerPrefs.GetFloat(GAME_MUSIC_VOLUME);
    }
}
