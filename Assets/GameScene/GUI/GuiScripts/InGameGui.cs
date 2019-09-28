using UnityEngine;
using UnityEngine.UI;

public class InGameGui : MonoBehaviour {
    //public GameObject statusBar;
    public PlayerManager manager = PlayerManager.Instance;
    public Slider slider;


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

}