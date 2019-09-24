using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float speed;
    public float sideWaySpeed;
    public bool boosted;
 
    public float speedForward;

    private PlayerEnum playerStats;
    private PlayerManager manager = PlayerManager.Instance;
    private PlayerMovePosition move = new PlayerMovePosition();
    
    public GameObject block;
    public GameObject obstacle;
    public GameObject coin;



    public int coins;
    private Rigidbody rb;
    float blockedTime = 0;

    private Floor floor = new Floor();
    bool blocked;
    

    void Start(){
        boosted = false;
        floor.generateFloor(block,obstacle, coin, (int)transform.position.x);
        
        rb = GetComponent<Rigidbody>();
       
        
       
        manager.setCoins(0);
    }


    
    void FixedUpdate(){
        if (!manager.getPlayerEnum().Equals(PlayerEnum.DEAD)) {
            floor.UpdateLevel(transform.position.x);
            rb.transform.position = move.moveDirection_X(transform.position);
            if (move.getMotion()) {
                rb.transform.position = move.moveToFinalPosition(transform.position);
            }

            if (Input.anyKeyDown) {
                float dirct = Input.GetAxisRaw("Horizontal");
                move.firstMotion(transform.position, dirct * block.transform.localScale.z);
            }
        }
        speedForward = move.getSpeed();
    }

    



    private float[] fillPositionsArray() {
        float localScale_z = block.transform.localScale.z;
        float[] positions = new float[3];
        int pos = 0;
        for (float depth = -localScale_z; depth <= localScale_z; depth += localScale_z) {
            positions[pos] = depth;
            pos++;
        }
        return positions;

    }


    void OnCollisionEnter(Collision col) {

        if (col.gameObject.name.Equals("Cylinder")) {
            Destroy(col.gameObject);
            manager.incCoins();
        }
    }




}
