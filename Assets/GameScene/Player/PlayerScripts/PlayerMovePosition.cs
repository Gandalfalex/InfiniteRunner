using UnityEngine;




/* This class simply calculates movements.
 * based on the players actuall position the player moves left or right to the ground centre
 * 
*/
public class PlayerMovePosition {


    private bool inMotion;
    private bool activated;
    private float speedForward = 9;
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



    /* the firstMotion Method sets the global variables, so the player cant move if their actuall movement isnt
     * finished.
     */
    public Vector3 firstMotion(Vector3 player, float destination) {
         if (!inMotion) {
            start = player.x;  
            inMotion = true;
            if (player.x == 0) {
                dest = destination;
            }
            else {
                dest = 0;
            }

            Vector3 nextPosition = player;
            nextPosition.x = dest;
            return Vector3.Lerp(player, nextPosition, 3 * Time.deltaTime);
         }
        return player;
    }

    /* Checks and calculates the positions and returns the next position.
     */
    public Vector3 moveToFinalPosition(Vector3 player) {
        if (inMotion && !siteHit) {
           
            if (Mathf.Abs(dest - player.x) <= 0.05f) {
                Vector3 nextPosition = player;
                nextPosition.x = dest;

                inMotion = false;
              

                return nextPosition;
            }
            else if (player.x != dest) {
                Vector3 nextPosition = player;
                nextPosition.x = dest;

                return Vector3.Lerp(player, nextPosition, speedForward * Time.deltaTime);
            } 
        }
        else if (siteHit){
            dest = start;
            siteHit = false;
        }
        return player;
    }

    /* calculates and increments the forward speed.
     */
    public Vector3 moveDirection_z(Vector3 player) {
        float tempspeed = speedForward + (float)(player.z / 500);
        Vector3 moveForward = new Vector3(player.x, player.y, player.z + speedForward * Time.deltaTime);
        return moveForward;
    }




    /* calculates where the player hits an obstacle.
     */
    public HitDirection workWithCollision(Vector3 player, Vector3 collision, Vector3 obstacle, float localScale_x) {
        Vector3 temp = (collision-player);
        if (temp.x != 0) {
            return HitDirection.SITE;
        }
        else if (Mathf.Abs(collision.x) <= (obstacle.x) && Mathf.Abs(collision.x) >= (obstacle.x - localScale_x)) {
            return HitDirection.FRONT;
        }
        return HitDirection.UNKNOWN;
    }

}
