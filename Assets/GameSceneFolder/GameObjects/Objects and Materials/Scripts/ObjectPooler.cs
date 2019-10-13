using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler  {

    private Dictionary<ItemType, List<GameObject>> itemDictionary = new Dictionary<ItemType, List<GameObject>>();


    public static ObjectPooler instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<ObjectPooler> lazy = new System.Lazy<ObjectPooler>(() => new ObjectPooler());


    private ObjectPooler() { }
 

    public void SetGameObjects(GameObject[] gameObjects) {
        foreach (GameObject item in gameObjects) {  
            ObjectStatsInterface tempInterface = item.GetComponent<ObjectStatsInterface>();
            GameObject itemHolder = new GameObject(tempInterface.GetItemType().ToString());
            for (int i = 0; i < tempInterface.GetRecommendedListSize(); i++) {
                GameObject temp = (GameObject) MonoBehaviour.Instantiate(item);
                temp.SetActive(false);
                if (itemDictionary.ContainsKey(tempInterface.GetItemType())) {
                    itemDictionary[tempInterface.GetItemType()].Add(temp);
                }
                else {
                    List<GameObject> tempList = new List<GameObject>() {temp};
                    itemDictionary.Add(tempInterface.GetItemType(), tempList);
                }
                temp.transform.parent = itemHolder.transform;
            }
        }
    }


    public GameObject getOutOfObjectPool(ItemType item) {

        if (itemDictionary.ContainsKey(item)) {
            foreach (GameObject game in itemDictionary[item]) {
                if (!game.activeInHierarchy) {
                    try {
                        game.GetComponent<Rigidbody>().useGravity = false;
                        game.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    catch(MissingComponentException) {}
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
