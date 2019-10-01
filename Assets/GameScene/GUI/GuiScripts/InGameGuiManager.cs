using UnityEngine;
using UnityEngine.UI;

public class InGameGuiManager : MonoBehaviour {
    //public GameObject statusBar;
    public PlayerManager manager = PlayerManager.Instance;
    public Slider slider;
    public GameObject panelInGame;
    public GameObject panelPausedGame;
    private int lastCoinCount;

    public int coinsToFillSlider;

    private void Awake() {
        panelInGame.SetActive(true);
        panelPausedGame.SetActive(false);
        slider.maxValue = 1f;
    }

    void FixedUpdate() {
        float temp = manager.getCoins() - lastCoinCount;
        float coins = (float)(temp / coinsToFillSlider);
        slider.value = coins;
    }



    public void activateBoost() {
        Debug.Log("clearing the way");
        if((manager.getCoins() - lastCoinCount) / coinsToFillSlider == 1) {
            manager.setPlayerEnum(PlayerStatEnum.BOOSTED);
            lastCoinCount = manager.getCoins();
        }
    }


    public void pauseGame() {
        panelInGame.SetActive(false);
        panelPausedGame.SetActive(true);
        Time.timeScale = 0;
    }



}