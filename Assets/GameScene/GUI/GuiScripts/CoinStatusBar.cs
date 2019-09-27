using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinStatusBar : MonoBehaviour
{
    //public GameObject statusBar;
    public PlayerManager manager = PlayerManager.Instance;
    public Slider slider;


    private void Start() {
        slider.maxValue = 1f;
    }
    void FixedUpdate() {
        
        float coins = (float)manager.getCoins() / 100;
        slider.value = coins;

       
    }
}
