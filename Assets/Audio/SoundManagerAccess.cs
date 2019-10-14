using UnityEngine;

public static class SoundManagerAccess {
  
    //private static MonoB sound = new MonoB();

    public static void PlayAudio(string name) {
        //sound.GetSound().PlayAudioByName(name);
        
    }
    public static void PlayRandomAudioByType(SoundType soundType) {
     
        //sound.GetSound().PlayRandomAudioByType(soundType);
    }
    public static void PlayAudioWithPitch(string name, int pitch) {
        //sound.GetSound().PlayAudioWithPitch(name, pitch);
    }

    public static void StopAudioByType(SoundType soundType) {
       // sound.GetSound().StopAudioByType(soundType);
    }


    /*
    private class MonoB: MonoBehaviour {

        private Soundmanager soundmanager;
     
   
        public Soundmanager GetSound() {
            soundmanager = FindObjectOfType<Soundmanager>();
            if (soundmanager != null)
                return soundmanager;
            return null;
        }
    }*/
}
