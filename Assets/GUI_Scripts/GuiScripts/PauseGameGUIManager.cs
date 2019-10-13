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
    public void StartGame() {
        ObjectPooler.instance.kill();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void MainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void PauseGame() {
        Debug.Log("stop new Game");
        if (!panelPausedGame.activeInHierarchy) {
            Time.timeScale = 0;
            panelPausedGame.SetActive(true);
        }
    }
    public void ResumeGame() {
        Debug.Log("resume Game");
        if (panelPausedGame.activeInHierarchy && !panelInGame.activeInHierarchy) {
            Time.timeScale = 1;
            panelPausedGame.SetActive(false);
            panelInGame.SetActive(true);
        }
    }
}
