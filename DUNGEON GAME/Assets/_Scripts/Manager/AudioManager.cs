using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] AudioClipArray;                      // Array of audio clips

    private static Dictionary<string, AudioClip> _DicAudio; // Audio library (dictionary)
    private static AudioSource audioBGM;                    // Audio source
    private static AudioSource[] audioSources;

    //[Header("VOL")]
    //[Range(0, 1)]
    //public float volumeOfBGM;
    //[Range(0, 1)]
    //public float volumeOfEffect;

    //[Header("VOL Slider")]
    public Slider volumeSlider;
    public float Volume { get; set; }

    void Awake()
    {
        // Initialize the audio library: create space and add all elements from the audio clip array
        _DicAudio = new Dictionary<string, AudioClip>();
        foreach (var item in AudioClipArray)
        {
            _DicAudio.Add(item.name, item);
        }

        // Specify the audio source for background music
        audioBGM = GetComponent<AudioSource>();
        if (audioBGM == null)
            audioBGM = gameObject.AddComponent<AudioSource>();

        audioSources = GetComponents<AudioSource>();

        Volume = volumeSlider.value;
    }

    // Function to play sound effects:
    public void PlayEffect(string acName)
    {
        // When the passed name is not empty and is in the audio library
        if (_DicAudio.ContainsKey(acName) && !string.IsNullOrEmpty(acName))
        {
            AudioClip ac = _DicAudio[acName];
            PlayEffect(ac);
        }
    }

    private void PlayEffect(AudioClip ac)
    {
        if (ac)
        {
            // Traverse the current AudioSource components
            audioSources = gameObject.GetComponents<AudioSource>();

            // AudioSource[0] is occupied by BGM, so start from [1]
            for (int i = 1; i < audioSources.Length; i++)
            {
                // When there is an available audio source, use it to play the sound
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].loop = false;
                    audioSources[i].clip = ac;
                    audioSources[i].volume = Volume;
                    audioSources[i].Play();
                    return;
                }
            }

            // When there are no available audio sources, create a new one
            AudioSource newAs = gameObject.AddComponent<AudioSource>();
            newAs.loop = false;
            newAs.clip = ac;
            newAs.volume = Volume;
            newAs.Play();
        }
    }

    // Function to play background music (BGM):
    public void BGMPlay(string acName)
    {
        // When the passed name is not empty and is in the audio library
        if (_DicAudio.ContainsKey(acName) && !string.IsNullOrEmpty(acName))
        {
            AudioClip ac = _DicAudio[acName];
            BGMPlay(ac);
        }
    }

    private void BGMPlay(AudioClip ac)
    {
        if (ac)
        {
            audioBGM.clip = ac;
            audioBGM.loop = true;
            audioBGM.volume = Volume;
            audioBGM.Play();
        }
    }

    // Function to stop the current BGM playback:
    public void StopBGMPlay()
    {
        audioBGM.Stop();
    }

    // Function to set the volume:
    public void SetVolume()
    {
        Volume = volumeSlider.value;
        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].volume = Volume;
    }
}
