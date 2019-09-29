using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameGUIManager : MonoBehaviour {

    [SerializeField]
    private GameObject panelPausedGame;
    [SerializeField]
    private GameObject panelInGame;



    private void Awake() {
        panelPausedGame.SetActive(false);
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
        if (!panelPausedGame.activeInHierarchy) {
            Time.timeScale = 0;
            panelPausedGame.SetActive(true);
        }
    }
    public void resumeGame() {
        Debug.Log("resume Game");
        if (panelPausedGame.activeInHierarchy && !panelInGame.activeInHierarchy) {
            Time.timeScale = 1;
            panelPausedGame.SetActive(false);
            panelInGame.SetActive(true);
        }
    }
}
