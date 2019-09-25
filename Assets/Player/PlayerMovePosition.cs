using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePosition {


    private bool inMotion;
    private bool activated;
    private float speedForward =10;
    private bool siteHit;

    private float dest;
    private float start;



    public bool getMotion() {
        return inMotion;
    }

    public bool getSiteHit() {
        return siteHit;
    }
    public void setSiteHit(bool hit) {
        this.siteHit = hit;
    }

    public void setSpeed(float speed) {
        speedForward = speed;
    }
    public float getSpeed() {
        return speedForward;
    }

    public Vector3 firstMotion(Vector3 player, float destination) {
        

        if (!inMotion) {
            start = player.z;  
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
       
        if (inMotion && !siteHit) {
           
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
        else if (siteHit){
            dest = start;
            siteHit = false;
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




    public HitDirection workWithCollision(Vector3 player, Vector3 collision, Vector3 obstacle, float localScale_z) {

        Debug.Log(collision - player);
        Vector3 test = (collision-player);
        if (test.z != 0) {
            return HitDirection.SITE;
        }


       else if (Mathf.Abs(collision.z) <= (obstacle.z) && Mathf.Abs(collision.z) >= (obstacle.z - localScale_z)) {
            return HitDirection.FRONT;
       }


        return HitDirection.UNKNOWN;
    }

}
