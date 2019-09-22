using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float speed;
    public float sideWaySpeed;
    public bool boosted;
    private float floorWidth;
    public float speedForward;


    private bool isMoving;
    private PlayerEnum playerStats;
    private PlayerManager manager = PlayerManager.Instance;
    
    public GameObject block;
    public GameObject obstacle;
    public GameObject coin;



    public int coins;
    private Rigidbody rb;
    float moveTowards_z = 0;

    private Floor floor = new Floor();
    

    void Start(){
        boosted = false;
        floor.generateFloor(block,obstacle, coin, (int)transform.position.x);
        
        rb = GetComponent<Rigidbody>();
        floorWidth = block.transform.localScale.z;
        manager.setCoins(0);
    }


    
    void FixedUpdate(){

        speedForward = speed + rb.position.x / 800;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        floor.UpdateLevel(rb.transform.position.x);
      

       
        if (!isMoving) {
            Vector3 moveForward = rb.position;
            moveForward.x += speedForward * Time.deltaTime;
            rb.transform.position = moveForward;
        }
    
      if (!playerStats.Equals(PlayerEnum.DEAD)) {
    
            if (!isMoving) {
                if (Input.GetKey("a") && rb.position.z < floorWidth) {
                    moveToPosition(block.transform.localScale.z);
                }
                else if (Input.GetKey("d") && rb.position.z > -floorWidth) {
                    moveToPosition(-block.transform.localScale.z);
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
            manager.incCoins();
           
        }
        else if (col.gameObject.name.Equals("Obstacles(Clone)")) {
           // Debug.Log("hit it " + coins + "     " + rb.velocity.x);
        }
    }
    

    private void moveToPosition(float finalPosition_z) {
       
        Vector3 moveDirektion = rb.position;
        moveDirektion.x += speedForward * Time.deltaTime;
        moveDirektion.z += sideWaySpeed * Time.deltaTime * finalPosition_z;
        rb.transform.position = moveDirektion;


    }


    
    float speedT = 2f;

    IEnumerator Move(Vector3 offsetFromCurrent) {
        if (isMoving) yield break; // exit function
        isMoving = true;
        Vector3 from = transform.position;
        Vector3 to = from + offsetFromCurrent;
        to.x += speedForward * Time.deltaTime;
        for (float t = 0f; t < 1f; t += Time.deltaTime * speedT) {
            to.x += speedForward * Time.deltaTime;
            transform.position = to;
            yield return null;
        }
        isMoving = false;
    }

    private float[] fillPositionsArray(float localScale_z) {
        float[] positions = new float[3];
        int pos = 0;
        for (float depth = -localScale_z; depth <= localScale_z; depth += localScale_z) {
            positions[pos] = depth;
            pos++;
        }
        return positions;

    }







    }
