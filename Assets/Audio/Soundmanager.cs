using System;
using UnityEngine;
using UnityEngine.Audio;


public class Soundmanager : MonoBehaviour{

    public Sound[] sounds;



    void Awake() {
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds) {
            sound.audio = gameObject.AddComponent<AudioSource>();
            sound.audio.clip = sound.clip;
            sound.audio.volume = sound.volume;
            sound.audio.pitch = sound.pitch;
        }
    }


    public void playAudio(string name) {
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        temp.audio.Play();
    }
}
