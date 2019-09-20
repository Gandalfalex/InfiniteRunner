using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionEnter : MonoBehaviour{
   // private PlayerStats playerStat = PlayerStats.getInstance();


    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("Sphere")) {
            Debug.Log("Did It");
        }
    }
}

