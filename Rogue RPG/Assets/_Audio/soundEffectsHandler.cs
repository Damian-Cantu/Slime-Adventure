using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundEffectsHandler : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource effectsSource;

    public float musicVolume = 1;
    public float soundfxVolume = 1;

    [System.Serializable]
    public class audioFile
    {
        public string tag;
        public AudioClip clip;
    }

    public List<audioFile> soundEffectsList;
    public List<audioFile> musicList;

    Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> music = new Dictionary<string, AudioClip>();

    public static soundEffectsHandler Instance;
    void Awake()
    {
        Instance = this;

        musicSource.volume = musicVolume;
        effectsSource.volume = soundfxVolume;

        foreach (audioFile AudioFile in soundEffectsList)
        {
            soundEffects.Add(AudioFile.tag, AudioFile.clip);
        }

        foreach (audioFile AudioFile in musicList)
        {
            music.Add(AudioFile.tag, AudioFile.clip);
            //Debug.Log(AudioFile.tag);
        }

        string startScene = SceneManager.GetActiveScene().name;

        switch (startScene)
        {
            case "StartMenu":
                musicSource.clip = music["mainMenu"];
                break;
            case "Plains":
                musicSource.clip = music["plainsTheme"];
                break;
            case "Ice":
                musicSource.clip = music["iceTheme"];
                break;
            case "Lava":
                musicSource.clip = music["volcanoTheme"];
                break;
            case "Sand":
                musicSource.clip = music["desertTheme"];
                break;
            case "Village":
                musicSource.clip = music["townTheme"];
                break;
            case "DeathMenu":
                musicSource.clip = music["gameOver"];
                break;
            default:
                Debug.LogWarning("Unable to find specified audio clip");
                break;
        }

        musicSource.Play();
    }

    public void PlaySound(string soundClip)
    {
        //effectsSource.clip = soundEffects[soundClip];
        //effectsSource.Play();
        if (soundEffects.ContainsKey(soundClip))
        {
            effectsSource.PlayOneShot(soundEffects[soundClip]);
        }
        else
        {
            Debug.LogWarning("Unable to find specified audio clip");
        }
        

    }

    public void ChangeSong(string soundClip)
    {
        

        if (music.ContainsKey(soundClip))
        {
            musicSource.clip = music[soundClip];
        }
        else
        {
            Debug.LogWarning("Unable to find specified audio clip");
        }
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }
}