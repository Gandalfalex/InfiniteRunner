using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float speed;
    public float sideWaySpeed;
    public bool boosted;
    private float floorWidth;



    private bool isMoving;
    private PlayerEnum playerStats;
    
    public GameObject block;
    public GameObject obstacle;



    public int coins;
    public GameObject coin;
    private Rigidbody rb;

    private Floor floor = new Floor();
    

    void Start(){
        boosted = false;
        floor.generateFloor(block,obstacle, coin, (int)transform.position.x);
        
        rb = GetComponent<Rigidbody>();
        floorWidth = floor.getBlockDepth() * block.transform.localScale.z;
    }


    
    void FixedUpdate(){
        Debug.Log(rb.transform.position);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if(floor.getLastDrawnObject()-rb.position.x <= 20) {

        }


        if (!playerStats.Equals(PlayerEnum.DEAD)) {
    
            Vector3 moveForward = rb.position;
            moveForward.x += speed * Time.deltaTime;
            rb.transform.position = moveForward;

            if (!isMoving) {
                if (Input.GetKey("a") && rb.position.z < floorWidth) {
                   
                    moveToPosition(rb.position.z, rb.transform.localScale.z);
                }
                else if (Input.GetKey("d") && rb.position.z > -floorWidth) {
                    moveToPosition(rb.position.z, -rb.transform.localScale.z);
                }
                if (rb.transform.position.y < -5) {
                    playerStats = PlayerEnum.DEAD;
                }
            }
        }      
    }

    void OnCollisionEnter(Collision col) {

        if (col.gameObject.name.Equals("Cylinder")) {
            Destroy(col.gameObject,0.3f);
            coins++;    
        }
        else if (col.gameObject.name.Equals("Obstacles(Clone)")) {
           // Debug.Log("hit it " + coins + "     " + rb.velocity.x);
            if (rb.velocity.x == 0) {
                Debug.Log("hit it " + coins);
            }
        }
    }
    

    private bool moveToPosition(float actuallPosition_z, float finalPosition_z) {


        Vector3 moveDirektion = rb.position;
        moveDirektion.x += speed * Time.deltaTime;
        moveDirektion.z += sideWaySpeed * Time.deltaTime * finalPosition_z;
        rb.transform.position = moveDirektion;
        return true;

    }


   







}
