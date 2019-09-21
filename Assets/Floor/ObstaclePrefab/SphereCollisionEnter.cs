using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionEnter : MonoBehaviour{
   
    void OnCollisionEnter(Collision collision) {

        float contactPoint_x = collision.contacts[0].point.x;
        float contactPoint_z = collision.contacts[0].point.z;
        Debug.Log("Hit it");

        if (collision.gameObject.name.Equals("Sphere") && (contactPoint_x - transform.localScale.x%2) %transform.localScale.x  == 0 && Mathf.Abs(contactPoint_z) != 0.9f) {
            Debug.Log(contactPoint_x + "   " + transform.localScale.x + "<<<<<z>>>>>" + contactPoint_z + "   " + transform.localScale.z);
        }
    }
}

