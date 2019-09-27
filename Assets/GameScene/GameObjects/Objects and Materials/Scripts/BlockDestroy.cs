using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    private GameObject camPos;


    // Update is called once per frame
    void Update(){
        camPos = GameObject.Find("Sphere");
        if (transform.position.z < camPos.transform.position.z - 20) {
            gameObject.SetActive(false);   
        }
        
    }
}
