using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler :MonoBehaviour {


    private List<GameObject> coinList = new List<GameObject>();
    private List<GameObject> floorList = new List<GameObject>();
    private List<GameObject> obstacleList = new List<GameObject>();

 


    public static ObjectPooler Instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<ObjectPooler> lazy = new System.Lazy<ObjectPooler>(() => new ObjectPooler());


    private ObjectPooler() { }

    public void setCoinGameObject(GameObject coin) {
        for (int i = 0; i < 800; i++) {
            GameObject temp = (GameObject) Instantiate(coin);
            temp.SetActive(false);
            coinList.Add(temp);
        }
    }
    public void setFloorGameObject(GameObject floor) {
        for (int i = 0; i <300; i++) {
            GameObject temp = (GameObject) Instantiate(floor);
            temp.SetActive(false);
            floorList.Add(temp);
        }
    }
    public void setObstacleGameObject(GameObject obstacle) {
        for (int i = 0; i < 50; i++) {
            
            GameObject temp = (GameObject)  Instantiate(obstacle);
            temp.SetActive(false);
            obstacleList.Add(temp);
        }
    }





    public GameObject getOutOfObjectPool(ItemTypes item) {

        

        
        if (item.Equals(ItemTypes.FLOOR)) {
            for (int i = 0; i < floorList.Count; i++) {
                if (!floorList[i].activeInHierarchy) { 
                    return floorList[i];
                }
            }
        }
        else if (item.Equals(ItemTypes.COIN)) {
            for (int i = 0; i < coinList.Count; i++) {
                if (!coinList[i].activeInHierarchy) {
                    return coinList[i];
                }
            }
        }
        else if (item.Equals(ItemTypes.OBSTACLE)) {
            for (int i = 0; i < obstacleList.Count; i++) {
                if (!obstacleList[i].activeInHierarchy) {
                    return obstacleList[i];
                }
            }
        }
        return null;
    }




}
