using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* This Class sets all objects, who are behind the Player, to inactive.
 * this way the Objectpooler Class can reposition their location and 
 * set them to active if they are needed.
 * This Class is part of every Object
 * the Update Method should be called once a second for performance;
*/
public class BlockDestroy : MonoBehaviour
{

    private GameObject player;
    private float time;

    private void Start() {
        time = Time.time;
    }

    void Update(){
       
        if (Time.time - time > 1) {
            
            time = Time.time;
            player = GameObject.Find("Player");
            if (transform.position.z < player.transform.position.z - 20) {
                gameObject.SetActive(false);
            }
        }
        
    }
}
