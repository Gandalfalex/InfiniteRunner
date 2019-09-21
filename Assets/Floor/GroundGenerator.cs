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

    private CreateInstanzes god;


    public Groundgenerator(Vector3 obstacle_scale, float[] positions, GameObject floor, GameObject obstacle, GameObject coin) {
        this.obstacle_scale = obstacle_scale;
        this.positions = positions;
        noObstacle_z = Random.Range(0, positions.Length);
        noObstacle_x = Random.Range(4, 8);
        obstacleLenght = Random.Range(3, 6);
        coinSpawn = Random.Range(7, 11);
        coinSpawn_position = Random.Range(0, positions.Length);
        centerCoins = obstacle_scale.z / 2;
        god = new CreateInstanzes(floor, obstacle, coin);
    }

    
    public int getLastPosition() {
        return lastPositionOfObject;
    }

    public async void generateLevel() {
        
        int heigh = 0;
        int low = 0;

        int end = size + lastPositionOfObject;
        for (int i = lastPositionOfObject; i < end; i++) {

            if (coinSpawn <= 0) {
                noCoin--;
                if (noCoin == 0) {
                    coinSpawn = Random.Range(7, 11);
                    coinSpawn_position = Random.Range(0, positions.Length);

                    noCoin = 5;
                }
            }

            if (noObstacle_x > 0) {
                foreach (float pos in positions) {
                    god.Create(new Vector3(i * obstacle_scale.x, low, pos), 0);
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        god.Create(new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins), 2);
                        // god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position]- centerCoins), 2);
                        // god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position]- centerCoins), 2);
                    }
                }
                noObstacle_x--;
            }
            else if (noObstacle_x == 0 && obstacleLenght > 0) {
                foreach (float pos in positions) {
                    if (pos == positions[noObstacle_z]) {
                        god.Create(new Vector3(i * obstacle_scale.x, low, pos), 0);
                        if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                            god.Create(new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins), 2);
                            // god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position]- centerCoins), 2);
                            // god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position]- centerCoins), 2);
                        }
                    }
                    else {
                        god.Create(new Vector3(i * obstacle_scale.x, heigh, pos), 1);
                    }
                }
                obstacleLenght--;
            }
            else {
                foreach (float pos in positions) {
                    god.Create(new Vector3(i * obstacle_scale.x, low, pos), 0);
                    if (coinSpawn > 0 && pos == positions[coinSpawn_position]) {
                        god.Create(new Vector3(i * obstacle_scale.x, 1, positions[coinSpawn_position] - centerCoins), 2);
                        // god.Create(new Vector3(i * floor_scale_x + 1, 1, positions[coinSpawn_position]- centerCoins), 2);
                        // god.Create(new Vector3(i * floor_scale_x + 2, 1, positions[coinSpawn_position]- centerCoins), 2);
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
