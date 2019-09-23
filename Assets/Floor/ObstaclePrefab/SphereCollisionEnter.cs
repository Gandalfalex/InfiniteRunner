﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionEnter : MonoBehaviour{
   
    void OnCollisionEnter(Collision collision) {

        float contactPoint_x = collision.contacts[0].point.x;
        float contactPoint_z = collision.contacts[0].point.z;
        // Debug.Log("Hit it");

        GameObject game = collision.gameObject;
        Vector3 posSphere = game.transform.position;
        //Debug.Log((posSphere.x) + " ---- " + ((transform.position.x - transform.localScale.x)) + "--------" + contactPoint_x);
        foreach (ContactPoint contact in collision.contacts) {
           // print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            //Debug.Log(contact.point + "----" + posSphere + "----" + transform.position);
            if(posSphere.x+2 <= transform.position.x && Mathf.Abs(posSphere.y) - (transform.localScale.y/2) <= Mathf.Abs(transform.position.y)) {
                Debug.Log(contact.point + "----" + posSphere + "----" + transform.position);
            }


        }
        if(posSphere.x == transform.position.x - 2) {
            Debug.Log("frontt hot");
        }
        //if (collision.gameObject.name.Equals("Sphere") && (contactPoint_x - transform.localScale.x%2) %transform.localScale.x  == 0 && Mathf.Abs(contactPoint_z) != 0.9f) {
            //Debug.Log(contactPoint_x + "   " + transform.localScale.x + "<<<<<z>>>>>" + contactPoint_z + "   " + transform.localScale.z);
        //}
    }
}
