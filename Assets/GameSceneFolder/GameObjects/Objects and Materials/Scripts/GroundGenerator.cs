﻿using UnityEngine;
using System.Collections.Generic;



/*this class handles the leveldesign and generation.
 
 */
public class Groundgenerator {

    Vector3 floor_Scale;


    private int obstacles_z_start;
    private int obstacles_z_end;
    private int leftOrRightSite;

    private int coinSpawn_position;

    public int size = 40;
    private float[] positions = new float[3];
    private int lastPositionOfObject = 0;

    private ObjectPooler god;
    
    private List<KeyValuePair<ItemType, Vector2>> randomObjectsToSpawn = new List<KeyValuePair<ItemType, Vector2>>();
    private int indexObjectToSpawn = 0;

    /*Sets up random vars to generate the level
     */
    public Groundgenerator(GameObject[] gameObjects) {

        foreach (GameObject gObject in gameObjects) {
            ItemType itemType = gObject.GetComponent<ObjectStatsInterface>().getType();
            if (itemType.Equals(ItemType.FLOOR)) {
                floor_Scale = gObject.transform.localScale;
                FillPositionsArray(floor_Scale.x);   
            }
            if (gObject.GetComponent<ObjectStatsInterface>().getObjectClass().Equals(ObjectClass.OBSTACLE)) {
                KeyValuePair<ItemType, Vector2> temp = new KeyValuePair<ItemType, Vector2>(itemType, 
                    gObject.GetComponent<ObstacleInterface>().GetPositionAndHeight());

                if (!randomObjectsToSpawn.Contains(temp)) {
                    randomObjectsToSpawn.Add(temp);
                }
            }
        }
        god = ObjectPooler.instance;
        god.setGameObjects(gameObjects);
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

        for (float i = -floor_Scale.x; i <= floor_Scale.x; i+=floor_Scale.x) {
            HandleObjects(ItemType.FLOOR, new Vector3(i, low, lastPositionOfObject * floor_Scale.z));
        }
        HandleObjects(ItemType.COIN, new Vector3(positions[coinSpawn_position], heigh, lastPositionOfObject * floor_Scale.z));
        HandleObjects(ItemType.FLY_UP, new Vector3(0, 3, lastPositionOfObject * floor_Scale.z));
        if (lastPositionOfObject >= obstacles_z_start && lastPositionOfObject < obstacles_z_end) {
            KeyValuePair<ItemType, Vector2> temp = randomObjectsToSpawn[indexObjectToSpawn];
            HandleObjects(temp.Key, new Vector3(temp.Value.x * floor_Scale.x * leftOrRightSite, temp.Value.y, lastPositionOfObject * floor_Scale.z));   ////////////////////////////// temp.Value.y
        }
        else if (lastPositionOfObject > obstacles_z_end) {
            SetNewVariables();
        }
        lastPositionOfObject ++;
    }


    /* activates new Prefabs, sets them active
     */
    public void HandleObjects(ItemType item, Vector3 position) {
        GameObject temp = god.getOutOfObjectPool(item);
        if (temp != null) {
            temp.SetActive(true);
            temp.transform.position = position;
        }
    }
    public void HandleObjects(ItemType item, Vector3 position, int newValue) {
        GameObject temp = god.getOutOfObjectPool(item);
        if (temp != null && item.Equals(ItemType.COIN)) {
            temp.SetActive(true);
            temp.GetComponent<CoinInterface>().setValue(newValue);
            temp.transform.position = position;
            temp.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
        obstacles_z_end = obstacles_z_start + Random.Range(2, 6);
        coinSpawn_position = Random.Range(0, positions.Length);
        int temp = Random.Range(-1, 1);
        if (temp == 0)
            leftOrRightSite = 1;
        else leftOrRightSite = -1;
    }
}