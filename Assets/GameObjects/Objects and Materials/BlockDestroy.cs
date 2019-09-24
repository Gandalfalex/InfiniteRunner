using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    private GameObject camPos;


    // Update is called once per frame
    void Update(){
        camPos = GameObject.Find("Sphere");
        if (transform.position.x < camPos.transform.position.x - 20) {
            this.gameObject.SetActive(false);   
        }
        
    }
}
