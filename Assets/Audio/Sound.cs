using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Sound {
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip clip;

    public string name;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    
    public SoundType soundType;

}
