using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An audio source for the entire game
/// </summary>
public class GameAudioSource : MonoBehaviour
{
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    public AudioSource theme;

	void Awake()
    {
        if (!AudioManager.audioInitialized)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(audioSource);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
