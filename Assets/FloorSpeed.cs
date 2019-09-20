using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpeed : MonoBehaviour
{
    public float speed;
    void Start(){
        speed = 0.8f; 
    }

    // Update is called once per frame
    void Update() {
        float movement = speed * Time.deltaTime;
        Vector3 vector = new Vector3(movement, 1, 1);
    }
}
