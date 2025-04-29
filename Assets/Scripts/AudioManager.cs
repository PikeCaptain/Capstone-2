using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioMixer masterMixer;

    // SINGLETON PATTERN
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        masterMixer.SetFloat("MasterVol", PreferencesManager.GetMasterVolume());
        masterMixer.SetFloat("MusicVol", PreferencesManager.GetMusicVolume());
    }

    public void ChangeSoundVolume(float soundLevel)
    {
        masterMixer.SetFloat("MasterVol", soundLevel); // Changes Volume
        PreferencesManager.SetMasterVolume(soundLevel); // Saves Player Pref
    }

    public void ChangeMusicVolume(float soundLevel)
    {
        masterMixer.SetFloat("MusicVol", soundLevel); // Changes Volume
        PreferencesManager.SetMasterVolume(soundLevel); // Saves Player Pref
    }

}
