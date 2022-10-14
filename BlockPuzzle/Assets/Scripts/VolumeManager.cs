using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private string FirstPlay= "FirstPlay";
    private string MusicPref= "MusicPref";

    private int firstPlayInt;
    public Slider musicSlider;
    private float musicFloat;
    public AudioSource musicAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            musicFloat = 0.25f;
            musicSlider.value = musicFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);

        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;

        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSettings();
        }
    }

    public void UpdateSound()
    {
        musicAudio.volume = musicSlider.value;
    }
}
