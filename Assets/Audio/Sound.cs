using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Sound {
    [HideInInspector]
    public AudioSource audio;
    public AudioClip clip;

    public string name;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

}
