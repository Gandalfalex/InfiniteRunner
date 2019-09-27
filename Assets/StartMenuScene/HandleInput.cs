using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleInput : MonoBehaviour { 
   
   
    public async void startGame() {
        SceneManager.LoadScene("GameScene");
    }


  
    public void exitGame() {
        Application.Quit();
    }


}
