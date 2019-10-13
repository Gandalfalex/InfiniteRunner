using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void startGame() {
        ObjectPooler.instance.kill();
        Debug.Log("starting new Game");
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame() {
        Application.Quit();
    }
}
