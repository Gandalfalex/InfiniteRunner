using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor {
    
    public int size = 100;
    public int blockDepth = 1;


    private CreateInstanzes god;
    
    public void generateFloor(GameObject block, GameObject obstacle) {
        Debug.Log("Generating");

        god = new CreateInstanzes(block,obstacle);
        int obstacle_Pos = Random.Range(8, 11);
        int obstacle_lenght = 1; //Random.Range(3, 4);
        int height = 1;
        int low = 0;
        //int pos = 0;


        float blockRange = blockDepth * block.transform.localScale.z;
        float localScale_z = block.transform.localScale.z;
        bool create_Obstacle = false;
        for (int i = 0; i <= size; i++) {

            float xpos = i * block.transform.localScale.x;

            if (i < 2) {
                for (float depth = -blockRange; depth <= blockRange; depth+= localScale_z) {
                    god.Create(new Vector3(xpos, low, depth));
                }
            }
            else {
                if(i% obstacle_Pos == 0 || create_Obstacle) {
                    create_Obstacle = true;
                    for(float depth = -blockRange; depth <= blockRange; depth += localScale_z) {
                        if(depth == 0) {
                            god.Create(new Vector3(xpos, low, depth));
                        }
                        else {
                            god.Create(new Vector3(xpos, height, depth));
                        } 
                    }
                    obstacle_lenght--;
                    
                    if(obstacle_lenght <= 0) {
                        create_Obstacle = false;
                        obstacle_Pos = Random.Range(4, 7);
                        obstacle_lenght = Random.Range(3, 6);
                    }
                }
                else {
                    for (float depth = -blockRange; depth <= blockRange; depth += localScale_z) {
                        god.Create(new Vector3(xpos, low, depth));
                    }
                }
            }
        }
    }

    public int getBlockDepth() {
        return this.blockDepth;
    }




    private class CreateInstanzes : MonoBehaviour {
        private GameObject floor;
        private GameObject obstacle;

        public CreateInstanzes(GameObject floor, GameObject obstacle) {
            this.floor = floor;
            this.obstacle = obstacle;
        }

        public void Create(Vector3 vector3) {
            if (vector3.y == 1) { 
                Instantiate(obstacle, vector3, Quaternion.identity);
            }
            else {
                Instantiate(floor, vector3, Quaternion.identity);
            }
        }
    }
}
