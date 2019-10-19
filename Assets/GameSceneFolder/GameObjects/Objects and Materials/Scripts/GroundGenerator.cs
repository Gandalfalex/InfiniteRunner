using UnityEngine;
using System.Collections.Generic;



/*this class handles the leveldesign and generation.
 
 */
public class Groundgenerator {

    Vector3 floor_Scale;


    private int obstacles_z_start;
    private int leftOrRightSite;

    private int coinSpawn_position;

    public int size = 10;
    private float[] positions = new float[3];
    private int lastPositionOfObject = 0;

    private ObjectPooler god;
    
    private List<KeyValuePair<ItemType, Vector2>> randomObjectsToSpawn = new List<KeyValuePair<ItemType, Vector2>>();
    private int indexObjectToSpawn = 0;


    public bool raiseEvent { get; set; }

    /*Sets up random vars to generate the level
     */
    public Groundgenerator(GameObject[] gameObjects) {
        raiseEvent = false;
        foreach (GameObject gObject in gameObjects) {
            ItemType itemType = gObject.GetComponent<IObjectStatsInterface>().GetItemType();
            if (itemType.Equals(ItemType.FLOOR)) {
                floor_Scale = gObject.transform.localScale;
                FillPositionsArray(floor_Scale.x);   
            }
            if (gObject.GetComponent<IObjectStatsInterface>().GetObjectClass().Equals(ObjectClassType.OBSTACLE)) {
                KeyValuePair<ItemType, Vector2> temp = new KeyValuePair<ItemType, Vector2>(itemType, 
                    gObject.GetComponent<IObstacleInterface>().GetPositionAndHeight());

                if (!randomObjectsToSpawn.Contains(temp)) {
                    randomObjectsToSpawn.Add(temp);
                }
            }
        }
        god = ObjectPooler.instance;
        god.SetGameObjects(gameObjects);
        SetNewVariables();
    }

    
    public int GetLastPosition() {
        return lastPositionOfObject;
    }
 

    /*Generates the first part. The rest will be generated at run time
     */
    public void GenerateLevel() { 
        int low = 0;
        for (int i = 0; i < size; i++) {
            int iScaler = i + lastPositionOfObject;
            foreach (float pos in positions) {
                HandleObjects(ItemType.FLOOR, new Vector3(pos, low, iScaler * floor_Scale.z));
            }
        }
        lastPositionOfObject = size;

    }



    /*based on their position, this method only creates a small part of the map
     */
    public void UpdateAtRuntime() {
        int heigh = 1;
        int low = 0;
        raiseEvent = false;
        for (float i = -floor_Scale.x; i <= floor_Scale.x; i+=floor_Scale.x) {
            HandleObjects(ItemType.FLOOR, new Vector3(i, low, lastPositionOfObject * floor_Scale.z));
        }
 
        HandleObjects(ItemType.COIN, new Vector3(positions[coinSpawn_position], heigh, lastPositionOfObject * floor_Scale.z));
        
        if (lastPositionOfObject == obstacles_z_start) {
            KeyValuePair<ItemType, Vector2> temp = randomObjectsToSpawn[indexObjectToSpawn];
            HandleObjects(temp.Key, new Vector3(temp.Value.x * floor_Scale.x * leftOrRightSite, temp.Value.y, lastPositionOfObject * floor_Scale.z));   
        }
        else if (lastPositionOfObject > obstacles_z_start) {
            SetNewVariables();
        }
        //if (obstacles_z_start - 4 == lastPositionOfObject && Random.Range(0,10) == 5) {
        if (obstacles_z_start - 4 == lastPositionOfObject) { 
            if (HandleObjects(ItemType.FLY_UP, new Vector3(0, heigh, lastPositionOfObject * floor_Scale.z)))
                raiseEvent = true;
        }
       
        lastPositionOfObject++;
    }

    /* activates new Prefabs, sets them active
     */
    public bool HandleObjects(ItemType item, Vector3 position) {
        GameObject temp = god.getOutOfObjectPool(item);
        if (temp != null) {
            temp.SetActive(true);
            temp.transform.position = position;
            return true;
        }
        return false;
    }
   

    /* sets the objectpooler to default
     */
    public void DestroyObjectPooler() {
        god.kill();
    }


    private void FillPositionsArray(float localScale_x) {
        positions[0] = -localScale_x;
        positions[1] = 0;
        positions[2] = localScale_x;
    }

    private void SetNewVariables() {
        indexObjectToSpawn = Random.Range(0, randomObjectsToSpawn.Count);
        obstacles_z_start = lastPositionOfObject + Random.Range(6, 10);
        coinSpawn_position = Random.Range(0, positions.Length);
        int temp = Random.Range(-1, 1);
        if (temp == 0)
            leftOrRightSite = 1;
        else
            leftOrRightSite = -1;
    }

}
