using UnityEngine;

public class Floor {

  
    private Groundgenerator groundgenerator;
    private bool stopGenerating = false;
   

    public void generateFloor(GameObject block, GameObject obstacle, GameObject coin, int spherePosition) {
       
        groundgenerator = new Groundgenerator(obstacle.transform.localScale, fillPositionsArray(obstacle.transform.localScale.x), block, obstacle, coin);
        groundgenerator.generateLevel();
       
    }



    private float[] fillPositionsArray(float localScale_x) {
        float[] positions = new float[3];
        int pos = 0;
        for (float depth = -localScale_x; depth <= localScale_x; depth += localScale_x) {
            positions[pos] = depth;
            pos++;
        }
        return positions;
    }





    /* if the distance between the player and the last active object is less then 10
     * then !stopGenerating 
     * else start generating the new level
     */
    public void UpdateLevel(float spherePosition_z, float localScale_z) {
      
        if(groundgenerator.getLastPosition() * localScale_z > spherePosition_z + 10) {
            stopGenerating = false;
        }

        if (groundgenerator.getLastPosition() * localScale_z - spherePosition_z < 200 && !stopGenerating) {
            stopGenerating = true;

            groundgenerator.UpdateAtRuntime();  
        }
    }

    public void setMotionToZero() {

    }

}