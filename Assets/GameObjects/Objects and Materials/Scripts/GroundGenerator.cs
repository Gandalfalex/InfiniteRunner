using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundgenerator {

    Vector3 obstacle_scale;


    private int noObstacle_z;
    private int obstacles_x_start;
    private int obstacles_x_end;

    private int coinSpawn;
    private int coinSpawn_position;
    private int noCoin = 5;

    private float centerCoins;

    public int size = 30;
    private float[] positions;
    private int lastPositionOfObject = 0;

    private bool generate;

    private ObjectPooler god;
    



    public Groundgenerator(Vector3 obstacle_scale, float[] positions, GameObject floor, GameObject obstacle, GameObject coin) {
        this.obstacle_scale = obstacle_scale;
        this.positions = positions;


        noObstacle_z = Random.Range(0, positions.Length);
        obstacles_x_start = Random.Range(6, 10);
        obstacles_x_end = obstacles_x_start + Random.Range(2, 6);


        coinSpawn = Random.Range(7, 11);
        coinSpawn_position = Random.Range(0, positions.Length);
        centerCoins = obstacle_scale.z / 2;


        god = ObjectPooler.instance;
        god.setCoinGameObject(coin);
        god.setFloorGameObject(floor);
        god.setObstacleGameObject(obstacle);


    }

    
    public int getLastPosition() {
        return lastPositionOfObject;
    }
 
    public void generateLevel() { 
        int heigh = 1;
        int low = 0;
     
        for (int i = 0; i < size; i++) {

            int iScaler = i + lastPositionOfObject;
            foreach (float pos in positions) {
                handleObjects(ItemTypes.FLOOR, new Vector3(iScaler * obstacle_scale.x, low, pos));
            }
            if( i >= obstacles_x_start && i < obstacles_x_end) {
                foreach (float pos in positions) {
                    if (pos != positions[noObstacle_z]) {
                        handleObjects(ItemTypes.OBSTACLE, new Vector3(iScaler * obstacle_scale.x, heigh, pos));
                    }
                }
            }
            else if(i > obstacles_x_end) {
                obstacles_x_start = i +  Random.Range(6, 10);
                obstacles_x_end = obstacles_x_start + Random.Range(2, 6);
                noObstacle_z = Random.Range(0, positions.Length);
            }
        }
        lastPositionOfObject = size;
    }


    public void UpdateOnRuntime() {
        int heigh = 1;
        int low = 0;



        int offset = 2;


        foreach (float pos in positions) {
            handleObjects(ItemTypes.FLOOR, new Vector3(lastPositionOfObject * obstacle_scale.x, low, pos));
        }

        if (lastPositionOfObject >= obstacles_x_start && lastPositionOfObject < obstacles_x_end) {
            foreach (float pos in positions) {
                if (pos != positions[noObstacle_z]) {
                    handleObjects(ItemTypes.OBSTACLE, new Vector3(lastPositionOfObject * obstacle_scale.x, heigh, pos));
                }
            }
        }
        else if (lastPositionOfObject > obstacles_x_end) {
            obstacles_x_start = lastPositionOfObject + Random.Range(6, 10);
            obstacles_x_end = obstacles_x_start + Random.Range(2, 6);
            noObstacle_z = Random.Range(0, positions.Length);
            coinSpawn_position =Random.Range(0, positions.Length);
        }


        if(lastPositionOfObject + offset < obstacles_x_start) {
            handleObjects(ItemTypes.COIN, new Vector3(lastPositionOfObject * obstacle_scale.x, heigh, (positions[coinSpawn_position] - centerCoins)));
        }


        lastPositionOfObject ++;
    }



    public void handleObjects(ItemTypes item, Vector3 position) {

        GameObject temp = god.getOutOfObjectPool(item);
       
        if (temp != null) {
            temp.SetActive(true);
            temp.transform.position = position;
            temp.transform.rotation = new Quaternion();
        }
    }

}
