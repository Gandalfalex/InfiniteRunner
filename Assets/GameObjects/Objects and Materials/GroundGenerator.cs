using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundgenerator {

    Vector3 obstacle_scale;
    private int noObstacle_z;
    private int noObstacle_x;
    private int obstacleLenght;

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
        noObstacle_x = Random.Range(4, 8);
        obstacleLenght = Random.Range(3, 6);
        coinSpawn = Random.Range(7, 11);
        coinSpawn_position = Random.Range(0, positions.Length);
        centerCoins = obstacle_scale.z / 2;


        god = ObjectPooler.Instance;
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

        int end = size + lastPositionOfObject;
        for (int i = lastPositionOfObject; i < end; i++) {
            //First Stage sets the coins
            if (coinSpawn <= 0) {
                noCoin--;
                if (noCoin == 0) {
                    coinSpawn = Random.Range(7, 11);
                    coinSpawn_position = Random.Range(0, positions.Length);

                    noCoin = 5;
                }
            }
            // the free space between after spawn
            if(i-lastPositionOfObject < 5) {
                foreach (float pos in positions) { 
                   handleObjects(ItemTypes.FLOOR,new Vector3(i * obstacle_scale.x, low, pos));
                }
            }
            //conditions for no obstacle
            else if (noObstacle_x > 0) {
                foreach (float pos in positions) {
                    handleObjects(ItemTypes.FLOOR, new Vector3(i * obstacle_scale.x, low, pos));
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        handleObjects(ItemTypes.COIN, new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins));
                    }
                }
                noObstacle_x--;
            }
            //now to position the obstacles
            else if (noObstacle_x == 0 && obstacleLenght > 0) {
                foreach (float pos in positions) {
                    if (pos == positions[noObstacle_z]) {
                        handleObjects(ItemTypes.FLOOR, new Vector3(i * obstacle_scale.x, low, pos));
                        if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                            handleObjects(ItemTypes.COIN, new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins));
                        }
                    }
                    else {
                       handleObjects(ItemTypes.OBSTACLE,new Vector3(i * obstacle_scale.x, heigh, pos));
                    }
                }
                obstacleLenght--;
            }
            //reset the variables, just Instant for this iteration
            else {
                foreach (float pos in positions) {
                    handleObjects(ItemTypes.FLOOR, new Vector3(i * obstacle_scale.x, low, pos));
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        handleObjects(ItemTypes.COIN, new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins));
                    }
                }
                noObstacle_z = Random.Range(0, positions.Length);
                noObstacle_x = Random.Range(4, 8);
                obstacleLenght = Random.Range(3, 6);
            }
            coinSpawn--;
        }
        lastPositionOfObject += size;
    }


    public void SetSize(int size) {
        this.size = size;
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
