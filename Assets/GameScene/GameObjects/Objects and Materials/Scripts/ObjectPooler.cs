using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler  {

    private Dictionary<ItemTypes, List<GameObject>> itemDictionary = new Dictionary<ItemTypes, List<GameObject>>();


    public static ObjectPooler instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<ObjectPooler> lazy = new System.Lazy<ObjectPooler>(() => new ObjectPooler());


    private ObjectPooler() { }
 

    public void setGameObjects(GameObject[] gameObjects) {
        foreach (GameObject item in gameObjects) {  
            ObjectStatsInterface tempInterface = item.GetComponent<ObjectStatsInterface>();
            GameObject itemHolder = new GameObject(tempInterface.getType().ToString());
            for (int i = 0; i < tempInterface.getRecommendedListSize(); i++) {
                GameObject temp = (GameObject) MonoBehaviour.Instantiate(item);
                temp.SetActive(false);
                if (itemDictionary.ContainsKey(tempInterface.getType())) {
                    itemDictionary[tempInterface.getType()].Add(temp);
                }
                else {
                    List<GameObject> tempList = new List<GameObject>();
                    tempList.Add(temp);
                    itemDictionary.Add(tempInterface.getType(), tempList);
                }
                temp.transform.parent = itemHolder.transform;
            }
        }
    }


    public GameObject getOutOfObjectPool(ItemTypes item) {

        if (itemDictionary.ContainsKey(item)) {
            foreach (GameObject game in itemDictionary[item]) {
                if (!game.activeInHierarchy) {
                    try {
                        game.GetComponent<Rigidbody>().useGravity = false;
                        game.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    catch { }

                    return game;
                }
            }
        }
        return null;
    }

    public void kill() {
        itemDictionary.Clear();
    }
}
