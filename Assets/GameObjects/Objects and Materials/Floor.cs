using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor {

  
    private Groundgenerator groundgenerator;
    private bool stopGenerating = false;
    private Vector3 obstacleScale;

    public void generateFloor(GameObject block, GameObject obstacle, GameObject coin, int spherePosition) {
        Debug.Log("Generating");
        groundgenerator = new Groundgenerator(obstacle.transform.localScale, fillPositionsArray(obstacle.transform.localScale.z), block, obstacle, coin);
        groundgenerator.generateLevel();
        obstacleScale = block.transform.localScale;
    }
     private float[] fillPositionsArray(float localScale_z) {
        float[] positions = new float[3];
        int pos = 0;
        for (float depth = -localScale_z; depth <= localScale_z; depth += localScale_z) {
            positions[pos] = depth;
            pos++;
        }
        return positions;
    }





    /* if the distance between the player and the last active object is less then 10
     * then !stopGenerating 
     * else start generating the new level
     */
    public void UpdateLevel(float spherePosition_x) {
       
        if(groundgenerator.getLastPosition()*3 > spherePosition_x + 10) {
            stopGenerating = false;
        }

        if (groundgenerator.getLastPosition() * obstacleScale.x - spherePosition_x < 60 && !stopGenerating) {

            stopGenerating = true;
            groundgenerator.SetSize(50);
            groundgenerator.UpdatePositions();  
        }
    }

    public void setMotionToZero() {

    }

}