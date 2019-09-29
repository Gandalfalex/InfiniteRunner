using UnityEngine;
using UnityEngine.UI;

public class InGameGuiManager : MonoBehaviour {
    //public GameObject statusBar;
    public PlayerManager manager = PlayerManager.Instance;
    public Slider slider;
    public GameObject panelInGame;
    public GameObject panelPausedGame;

    private void Awake() {
        panelInGame.SetActive(true);
        panelPausedGame.SetActive(false);
    }
    private void Start() {
        slider.maxValue = 1f;
    }
    void FixedUpdate() {
        float coins = (float)manager.getCoins() / 10;
        slider.value = coins;
    }



    public void activateBoost() {
        Debug.Log("clearing the way");
        manager.setCoins(0);
        manager.setPlayerEnum(PlayerStatEnum.BOOSTED);
    }


    public void pauseGame() {
        panelInGame.SetActive(false);
        panelPausedGame.SetActive(true);
        Time.timeScale = 0;
    }



}