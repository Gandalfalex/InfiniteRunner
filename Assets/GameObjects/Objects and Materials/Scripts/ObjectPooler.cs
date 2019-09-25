using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler  {


    private List<GameObject> coinList = new List<GameObject>();
    private List<GameObject> floorList = new List<GameObject>();
    private List<GameObject> obstacleList = new List<GameObject>();



    public static ObjectPooler instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<ObjectPooler> lazy = new System.Lazy<ObjectPooler>(() => new ObjectPooler());


    private ObjectPooler() { }

    public void setCoinGameObject(GameObject coin) {
        GameObject coinHolder = new GameObject("CoinHolder");


        for (int i = 0; i < 800; i++) {
            GameObject temp = (GameObject)MonoBehaviour.Instantiate(coin);
            temp.SetActive(false);
            coinList.Add(temp);
            temp.transform.parent = coinHolder.transform;
        }
    }
    public void setFloorGameObject(GameObject floor) {
        GameObject floorHolder = new GameObject("Floor Holder");

        for (int i = 0; i <300; i++) {
            GameObject temp = (GameObject)MonoBehaviour.Instantiate(floor);
            temp.SetActive(false);
            floorList.Add(temp);
            temp.transform.parent = floorHolder.transform;
        }
    }
    public void setObstacleGameObject(GameObject obstacle) {
        GameObject obstacleHolder = new GameObject("Obstacle Holder");

        for (int i = 0; i < 50; i++) {   
            GameObject temp = (GameObject)MonoBehaviour.Instantiate(obstacle);
            temp.SetActive(false);
            obstacleList.Add(temp);
            temp.transform.parent = obstacleHolder.transform;
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
