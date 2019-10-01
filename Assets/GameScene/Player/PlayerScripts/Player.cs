﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{

    [SerializeField]
    private Transform floorTransform;
   
    public GameObject cam;
    public Vector3 offset;
    public Vector3 rotation;

    public float speedForward;

    private PlayerStatEnum playerStats;
    private PlayerManager manager = PlayerManager.Instance;
    private PlayerMovePosition move = new PlayerMovePosition();

 

    private bool blocked;
    private float startpoint;
    private float directionRaw;


    void Start(){
        Debug.Log(Screen.currentResolution);
        manager.setCoins(0);
        move.setSpeed(speedForward);
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        FindObjectOfType<Soundmanager>().playAudio("background");
    }

    void FixedUpdate(){



        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);

        if (!manager.getPlayerEnum().Equals(PlayerStatEnum.PAUSED)) {
            transform.position = move.moveDirection_z(transform.position);

            if (move.getMotion()) {
                transform.position = move.moveToFinalPosition(transform.position);
            }

            if (Input.GetKey("a") || Input.GetKey("d")) {
                float dirct = Input.GetAxisRaw("Horizontal");
                move.firstMotion(transform.position, (dirct * floorTransform.localScale.x));
            }
          
           else if(Input.touchCount>0) {
                Touch touch = Input.touches[0];
               
                if (touch.phase.Equals(TouchPhase.Began) && !blocked) {
                   
                    blocked = true;
                    startpoint = touch.position.x;
                }
                else if (touch.phase.Equals(TouchPhase.Ended) && blocked) {
                  
                    blocked = true;
                    directionRaw = touch.position.x - startpoint;
                    float temp = directionRaw / Mathf.Abs(directionRaw);
                    move.firstMotion(transform.position, (temp * floorTransform.localScale.x));
                }
           }
        }
        speedForward = move.getSpeed();

        if(transform.position.y < - 2) {
            manager.setPlayerEnum(PlayerStatEnum.DEAD);
 
        }
        handleCamMovement();
    }


    void OnCollisionEnter(Collision col) {

        ItemTypes itemType = col.gameObject.GetComponent<ObjectStatsInterface>().getType();

        if (itemType.Equals(ItemTypes.COIN)) {
            
            if (col.gameObject.activeInHierarchy) {
                col.gameObject.SetActive(false);
                manager.incCoins();
                Debug.Log(manager.getCoins() +" " + col.gameObject.GetComponent<CoinInterface>().getValue()) ;
                FindObjectOfType<Soundmanager>().playAudio("Hit", manager.getCoins());
                return;
            } 
           
        }
        else if (!itemType.Equals(ItemTypes.FLOOR)) {
            HitDirection hit =  move.workWithCollision(transform.position, col.contacts[0].point, col.gameObject.transform.position, col.gameObject.transform.localScale.x);
            if (hit.Equals(HitDirection.SITE)) {
                Debug.Log(" site hit");
                move.setSiteHit(true);
                StartCoroutine(shakeCam(0.2f));
                Vibration.Vibrate(50);
                manager.setNearDeath(true);
            }
            else{//if (hit.Equals(HitDirection.FRONT)) {
                PlayerManager manager = PlayerManager.Instance;
                manager.setPlayerEnum(PlayerStatEnum.DEAD);
                Debug.Log("front hit");
                SceneManager.LoadScene("StartMenuScene");
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
