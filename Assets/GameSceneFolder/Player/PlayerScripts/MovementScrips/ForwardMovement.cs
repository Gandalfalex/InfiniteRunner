using UnityEngine;

public static class ForwardMovement {

    private static readonly float speed = 30;
    public static Vector3 Move(Vector3 player) {
        float tempSpeed = speed + (float)(player.z / 100);
        Vector3 moveForward = new Vector3(player.x, player.y, player.z + tempSpeed * Time.deltaTime);
        return moveForward;
    }
}
