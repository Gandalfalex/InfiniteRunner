using System;
using System.Collections.Generic;
using UnityEngine;


public class Soundmanager : MonoBehaviour{

    public Sound[] sounds;

    private Dictionary<SoundType, List<Sound>> soundDictionary = new Dictionary<SoundType, List<Sound>>();
    public float pitcher = 40;


    public Sound loopingSound;
    public bool gotLoopingSound;


    void Awake() {
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            if (soundDictionary.ContainsKey(sound.soundType)) {
                soundDictionary[sound.soundType].Add(sound);
            }
            else {
                List<Sound> temp = new List<Sound> {sound};
                soundDictionary.Add(sound.soundType, temp);
            }
        }
    }




    public void PlayAudioByName(string name) {
        Sound temp = Array.Find(sounds, sound => sound.name == name);
  
        if (temp != null) {
            temp.audioSource.Play();
            temp.audioSource.loop = temp.loop;
        }
    }

    public void PlayAudioWithPitch(string name, float pitch) {
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        if (temp != null) {         
            temp.audioSource.pitch = temp.pitch - pitch / pitcher;

            temp.audioSource.Play();
            temp.audioSource.loop = temp.loop;
        } 
    }

    public void PlayRandomAudioByType(SoundType type) {
        if (soundDictionary.ContainsKey(type)) {
            int i = soundDictionary[type].Count;
            int randomOne = UnityEngine.Random.Range(0, i);
            Sound temp = soundDictionary[type][randomOne];
            if (temp != null) {
                SetLoopingSound(temp);
                temp.audioSource.Play();
                temp.audioSource.loop = temp.loop;
    
            }
        }
    }


    private void SetLoopingSound(Sound sound) {
        if (gotLoopingSound) {
            StopAudio();
        }
        gotLoopingSound = true;
        loopingSound = sound;
    }
    public void StopAudioByType(SoundType type) {
        if (gotLoopingSound) {
            loopingSound.audioSource.Stop();
        }
    }
    private void StopAudio() {
        loopingSound.audioSource.Stop();
        gotLoopingSound = false;
    
    }




}
