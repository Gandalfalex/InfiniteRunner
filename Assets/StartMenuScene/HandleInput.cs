using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleInput : MonoBehaviour {


    public GameObject pausePanel;
    private void Start() {
        pausePanel.SetActive(false);
    }
    public void startGame() {
        ObjectPooler.instance.kill();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void exitGame() {
        Application.Quit();
    }

    public void mainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void pauseGame() {
        Debug.Log("stop new Game");
        //if (!pausePanel.activeInHierarchy) {
        Time.timeScale = 0;
            pausePanel.SetActive(true);
        //}
    }
    public void resumeGame() {
        Debug.Log("resume Game");
        // if (pausePanel.activeInHierarchy) {
        Time.timeScale = 1;
            pausePanel.SetActive(false);
        //}

    }
}
