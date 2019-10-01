using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public class Soundmanager : MonoBehaviour{

    public Sound[] sounds;



    void Awake() {
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }
    }


    public void playAudio(string name) {
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        if (temp != null) {
            temp.audioSource.Play();
            temp.audioSource.loop = temp.loop;
      
        }
    }

    public void playAudio(string name, int i) {
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        if (temp != null) {
            temp.audioSource.pitch = temp.pitch -(float)i / 20;

            temp.audioSource.Play();
           // yield return new WaitForSeconds(1.5f);
        
        } 
    }

  

}
