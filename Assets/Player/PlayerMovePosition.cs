using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePosition {


    private bool inMotion;
    private bool activated;
    private float speedForward =10;
    

    private float dest;

    
    
    public bool getMotion() {
        return inMotion;
    }

    public void setSpeed(float speed) {
        speedForward = speed;
    }
    public float getSpeed() {
        return speedForward;
    }

    public Vector3 firstMotion(Vector3 player, float destination) {
        

        if (!inMotion) {
           
            inMotion = true;
            if (player.z == 0) {
                dest = -destination;
            }
            else {
                dest = 0;
            }

            Vector3 nextPosition = player;
            nextPosition.z = dest;

            return Vector3.Lerp(player, nextPosition, 3 * Time.deltaTime);
        }
        return player;
    }


    public Vector3 moveToFinalPosition(Vector3 player) {
       
        if (inMotion) {
           
            if (Mathf.Abs(dest - player.z) <= 0.05f) {
                Vector3 nextPosition = player;
                nextPosition.z = dest;

                inMotion = false;
              

                return nextPosition;
            }
            else if (player.z != dest) {
                Vector3 nextPosition = player;
                nextPosition.z = dest;

                return Vector3.Lerp(player, nextPosition, speedForward * Time.deltaTime);
            }   
        }
        return player;
    }


    public Vector3 moveDirection_X(Vector3 player) {
        speedForward += player.x/ 1000 ;
        if (speedForward > 20) {
            speedForward = 20;
        }
        Vector3 moveForward = new Vector3(player.x + speedForward * Time.deltaTime, player.y, player.z);
        return moveForward;
    }


}
