using UnityEngine;

public class PauseGame : MonoBehaviour{
    private bool paused;
    
    public void Pause() {
        if (paused) {
            Time.timeScale = 1;
            paused = false;
        }
        else {
            Time.timeScale = 0;
            paused = true;
        }
    }
}
