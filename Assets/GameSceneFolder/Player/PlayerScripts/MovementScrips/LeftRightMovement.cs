using UnityEngine;

public static class LeftRightMoveMent {

    private static bool inMotion = false;
    private static bool siteHit = false;
    private static float startPoint;
    private static float destinationPoint;
    private static readonly float speed = 15;

    public static bool GetInMotion() { return inMotion; }
    public static void SetSiteHit() { siteHit = true; }

    public static Vector3 FirstMotion(Vector3 player, float destination) {
        if (!inMotion) {
            startPoint = player.x;
            inMotion = true;
            if (player.x == 0) {
                destinationPoint = destination;
            }
            else {
                destinationPoint = 0;
            }

            Vector3 nextPosition = player;
            nextPosition.x = destinationPoint;
            return Vector3.Lerp(player, nextPosition, speed * Time.deltaTime);
        }
        return player;
    }

    /* Checks and calculates the positions and returns the next position.
     */
    public static Vector3 ContinueMove(Vector3 player) {
        if (inMotion && !siteHit) {
            if (Mathf.Abs(destinationPoint - player.x) <= 0.05f) {
                Vector3 nextPosition = player;
                nextPosition.x = destinationPoint;
                inMotion = false;
                return nextPosition;
            }
            else if (player.x != destinationPoint) {
                Vector3 nextPosition = player;
                nextPosition.x = destinationPoint;
                return Vector3.Lerp(player, nextPosition, speed * Time.deltaTime);
            }
        }
        else if (siteHit) {
            destinationPoint = startPoint;
            siteHit = false;
        }
        return player;
    }


}
