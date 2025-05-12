using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;

    private void Start()
    {
        if(masterSlider != null)
        {
            masterSlider.value = PreferencesManager.GetMasterVolume();
        }
        if(musicSlider != null)
        {
            musicSlider.value = PreferencesManager.GetMusicVolume();
        }
    }

    public void ChangeSound(float soundLevel)
    {
        AudioManager.instance.ChangeSoundVolume(soundLevel);
    }

    public void ChangeMusic(float soundLevel)
    {
        AudioManager.instance.ChangeMusicVolume(soundLevel);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
