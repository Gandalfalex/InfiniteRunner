using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonCreator : MonoBehaviour
{


    private Button startGameBtn;
    private Button quiteGameBtn;



    void Awake(){
        startGameBtn.onClick.AddListener(startGame);
        startGameBtn.colors = new ColorBlock();

        GameObject btn = GameObject.Find("StartGameBtn");
        Vector2 display = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        btn.transform.position = display;
        btn.transform.localScale = new Vector2(500, 500);
        btn.active = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void startGame() {

    }
}
