using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator{
    private GameObject coin;
    public void GenerateCoins(GameObject coin) { 
        CoinInstanze god = new CoinInstanze(coin);
       
        for (int i = 0; i<=100; i++) {
            Vector3 vec = new Vector3(i, 1, -1);
            god.create(vec);
           
        }  
    }


    private class CoinInstanze : MonoBehaviour {
        private GameObject coin;
        public CoinInstanze (GameObject coin) {
            this.coin = coin;
        }
        public void create(Vector3 vec) {
            Instantiate(coin, vec, Quaternion.identity);
        }
    }
}
