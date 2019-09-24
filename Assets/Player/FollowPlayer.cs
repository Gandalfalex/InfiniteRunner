using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;

    void Start() {
        offset = transform.position - player.transform.position;
        Debug.Log(offset);
    }

    void LateUpdate() {

        float newXPosition = player.transform.position.x + offset.x-2;
        float newZPosition = player.transform.position.z - offset.z;

        transform.position = new Vector3(newXPosition, 5 , newZPosition);
    }



}
