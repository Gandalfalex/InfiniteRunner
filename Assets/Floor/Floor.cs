using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor {

    public int size = 50;
    public int blockDepth = 1;
    private float[] positions = new float[3];
    private int lastPositionOfObject = 0;

    private CreateInstanzes god;
    
    public void generateFloor(GameObject block, GameObject obstacle, GameObject coin, int spherePosition) {
        Debug.Log("Generating");
        god = new CreateInstanzes(block,obstacle, coin);
        if (lastPositionOfObject == 0) {
            createIt(block.transform.localScale.x, obstacle.transform.localScale.z);
        }
    }

    public void expandMap(int spherePosition) {
        if (lastPositionOfObject - spherePosition < 30) {
            createIt(1.8f,1.8f);
        }
    }


    public int getBlockDepth() {
        return this.blockDepth;
    }

    private void fillPositionsArray(float blockRange, float localScale_z) {
       int pos = 0;
       for (float depth = -blockRange; depth <= blockRange; depth += localScale_z) {
            positions[pos] = depth;
            Debug.Log(depth);
            pos++;
       }
    }



    public int getLastDrawnObject() {
        return lastPositionOfObject;
    }

   



    private void createIt(float floor_scale_x, float obstacle_scale_z) {

        fillPositionsArray(blockDepth * obstacle_scale_z, obstacle_scale_z);
        int heigh = 0;
        int low = 0;

        int noObstacle_z = Random.Range(0, positions.Length);
        int noObstacle_x = Random.Range(4, 8);
        int obstacleLenght = Random.Range(3, 6);
        int coinSpawn = Random.Range(7, 11);
        int coinSpawn_position = Random.Range(0, positions.Length);
        Debug.Log(coinSpawn_position + " after init " + coinSpawn + "     " + positions[coinSpawn_position] / 2);
        int noCoin = 5;

        float centerCoins = obstacle_scale_z / 2;
       
        for (int i = lastPositionOfObject; i < size+lastPositionOfObject; i++) {

            if(coinSpawn <= 0) {
                noCoin--;
                if (noCoin == 0) {
                    coinSpawn = Random.Range(7, 11);
                    coinSpawn_position = Random.Range(0, positions.Length);
                    Debug.Log(coinSpawn_position + " at RunTime");
                    noCoin = 5;
                }
            }

            if (noObstacle_x > 0) {                                                                
                foreach (float pos in positions) {
                    god.Create(new Vector3(i * floor_scale_x, low, pos), 0);
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        god.Create(new Vector3(i * floor_scale_x, 1, positions[coinSpawn_position]- centerCoins), 2);
                        god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position]- centerCoins), 2);
                        god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position]- centerCoins), 2);
                    }
                }
                noObstacle_x--;
            }
            else if (noObstacle_x == 0 && obstacleLenght > 0) {
                foreach (float pos in positions) {
                    if (pos == positions[noObstacle_z]) {
                        god.Create(new Vector3(i * floor_scale_x, low, pos), 0);
                        if (coinSpawn > 0 && pos==positions[coinSpawn_position]) {
                            god.Create(new Vector3(i * floor_scale_x, 1, positions[coinSpawn_position] - centerCoins), 2);
                            god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position] - centerCoins), 2);
                            god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position] - centerCoins), 2);
                        }
                    }
                    else {
                        god.Create(new Vector3(i * floor_scale_x, heigh, pos), 1);
                    }
                }
                obstacleLenght--;
            }
            else {
                foreach (float pos in positions) {
                    god.Create(new Vector3(i * floor_scale_x, low, pos), 0);
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        god.Create(new Vector3(i * floor_scale_x, 1, positions[coinSpawn_position] - centerCoins), 2);
                        god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position] - centerCoins), 2);
                        god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position] - centerCoins), 2);
                    }
                }
                noObstacle_z = Random.Range(0, positions.Length);
                noObstacle_x = Random.Range(4, 8);
                obstacleLenght = Random.Range(3, 6);       
            }
            coinSpawn--;
            lastPositionOfObject =  i * (int)floor_scale_x;
        }
    }


    private class CreateInstanzes : MonoBehaviour {
        private GameObject floor;
        private GameObject obstacle;
        private GameObject coin;


        public CreateInstanzes(GameObject floor, GameObject obstacle, GameObject coin) {
            this.floor = floor;
            this.obstacle = obstacle;
            this.coin = coin;
        }

        public void Create(Vector3 vector3, int pos) {
            if (pos == 0) {
                Instantiate(floor, vector3, Quaternion.identity);
            }
            else if (pos == 1) {
                Instantiate(obstacle, vector3, Quaternion.identity);
            }
            else {
                Instantiate(coin, vector3, Quaternion.identity);
            }
        }
    }
}
