using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

  
    public bool boosted;
 
    public float speedForward;

    private PlayerStatEnum playerStats;
    private PlayerManager manager = PlayerManager.Instance;
    private PlayerMovePosition move = new PlayerMovePosition();
    
    public GameObject block;
    public GameObject obstacle;
    public GameObject coin;
    public GameObject cam;

    public Vector3 offset;



    public int coins;
    float blockedTime = 0;

    private Floor floor = new Floor();
    bool blocked;
    

    void Start(){
        boosted = false;
        floor.generateFloor(block,obstacle, coin, (int)transform.position.x);
        manager.setCoins(0);
        offset = cam.transform.position - transform.position;
    }


    
    void FixedUpdate(){
        if (!manager.getPlayerEnum().Equals(PlayerStatEnum.DEAD)) {
            floor.UpdateLevel(transform.position.z, block.transform.localScale.z);
            transform.position = move.moveDirection_z(transform.position);

            if (move.getMotion()) {
                transform.position = move.moveToFinalPosition(transform.position);
            }

            if (Input.anyKeyDown) {
                float dirct = Input.GetAxisRaw("Horizontal");
                move.firstMotion(transform.position, (dirct * block.transform.localScale.x));
            }
        }
        speedForward = move.getSpeed();

        if(transform.position.y < -2) {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }
        handleCamMovement();
    }

    



    

    void OnCollisionEnter(Collision col) {

        if (col.gameObject.gameObject.name.Equals("Coin(Clone)")) {
            manager.incCoins();
            col.gameObject.gameObject.SetActive(false);
        }
        else if (col.gameObject.name.Equals("Obstacles(Clone)")) {
            HitDirection hit =  move.workWithCollision(transform.position, col.contacts[0].point, col.gameObject.transform.position, col.gameObject.transform.localScale.x);
            if (hit.Equals(HitDirection.SITE)) {
                move.setSiteHit(true);
                Debug.Log("sitehit");
                StartCoroutine(shakeCam(0.2f));
            }
            if (hit.Equals(HitDirection.FRONT)) {
                PlayerManager manager = PlayerManager.Instance;
                manager.setPlayerEnum(PlayerStatEnum.DEAD);
                Debug.Log("front hit");
            }
        }
    }


    public void handleCamMovement() {

        float newXPosition = transform.position.x - offset.x ;
        float newZPosition = transform.position.z - offset.z - 15 ;

        cam.transform.position = new Vector3(newXPosition, 5, newZPosition);
    }

    private IEnumerator shakeCam(float time) {


        
        float duration = 0f;
        while(duration < time) {
            Vector3 pos = cam.transform.position;
            float xMove = Random.Range(-1, 1)* 0.2f;
            float yMove = Random.Range(-1, 1)*0.2f ;
            cam.transform.localPosition = new Vector3(pos.x, pos.y + yMove, pos.z + xMove);
            duration += Time.deltaTime;
            yield return null;
        }

        handleCamMovement();
    }
}
