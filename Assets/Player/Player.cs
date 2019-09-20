using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float sideWaySpeed;
    public bool boosted;
    
    
    private bool isMoving;
    private PlayerEnum playerStats;
    
    public GameObject block;
    public GameObject obstacle;



    public int coins;
    public GameObject coin;
    private Rigidbody rb;

    private Floor floor = new Floor();
    private CoinGenerator coinClass = new CoinGenerator();



    void Start(){
        boosted = false;
        coinClass.GenerateCoins(coin);
        floor.generateFloor(block,obstacle);
        
        rb = GetComponent<Rigidbody>();
    }


    
    void FixedUpdate(){
        Vector3 vel = rb.velocity;
        vel.z = 0;
        rb.velocity = vel;
        if (!playerStats.Equals(PlayerEnum.DEAD)) {
    
            Vector3 moveForward = rb.position;
            moveForward.x += speed * Time.deltaTime;
            rb.AddForce(moveForward);


            if (!isMoving) {
                if (Input.GetKey("a") && rb.position.z < floor.getBlockDepth() * block.transform.localScale.z) {
                    moveToPosition(rb.position.z, rb.transform.localScale.z);
                }
                else if (Input.GetKey("d") && rb.position.z > -floor.getBlockDepth() * block.transform.localScale.z) {
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
            Destroy(col.gameObject);
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

        float directionalSpeed = finalPosition_z - actuallPosition_z;
        if(directionalSpeed != 0) {
            Vector3 moveDirektion = rb.position;
            moveDirektion.z = moveDirektion.z + sideWaySpeed *Time.deltaTime *directionalSpeed;
            rb.MovePosition(moveDirektion);
            return false;
        }
        else {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            return true;
        }
    }







}
