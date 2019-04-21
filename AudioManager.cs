using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager Loads all audio from resource folder to a variable
/// </summary>
public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    public static bool audioInitialized = false;


    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioInitialized = true;
        audioClips.Add(AudioClipName.AsteroidHit, 
            Resources.Load<AudioClip>("hit"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PlayerShot,
            Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioClipName.Thrust,
            Resources.Load<AudioClip>("thrust"));
    }

    public static void Play(AudioClipName name)  // call this public function along with clip name to play audio through audio source
    {
        audioSource.PlayOneShot(audioClips[name],1f);
    }

    public static void PlayThrust(AudioClipName name)  // To play thrust with low volume
    {
        audioSource.PlayOneShot(audioClips[name], 0.02f);
    }


}
