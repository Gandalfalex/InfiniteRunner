using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{

    [SerializeField]
    private Transform floorTransform;
   
    public GameObject cam;
    public Vector3 offset;
    public Vector3 rotation;

    public float speedForward;

    private PlayerManager manager = PlayerManager.Instance;
    private PlayerMovePosition move = new PlayerMovePosition();

 

    private bool blocked;
    private float startpoint;
    private float directionRaw;


    private void Awake(){
        manager.setCoins(0);
        move.setSpeed(speedForward);
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        FindObjectOfType<Soundmanager>().playAudio("background");
    }

    void FixedUpdate(){
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        HandleCamMovement();

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
        if (transform.position.y < - 2) {
            manager.setPlayerEnum(PlayerStatEnum.DEAD);
        }
    }


    void OnCollisionEnter(Collision col) {
        ObjectClass itemType = col.gameObject.GetComponent<ObjectStatsInterface>().getObjectClass();
        if (itemType.Equals(ObjectClass.OBSTACLE)) {
            HandleCollision(move.workWithCollision(transform.position, col.contacts[0].point, 
               col.gameObject.transform.position, col.gameObject.transform.localScale.x));
        }
    }


    public void HandleCamMovement() {
        float newXPosition = transform.position.x/10;
        float newZPosition = transform.position.z - offset.z;
        cam.transform.position = new Vector3(newXPosition, 5, newZPosition);
    }

    private IEnumerator ShakeCam(float time) {

        float duration = 0f;
        while(duration < time) {
            Vector3 pos = cam.transform.position;
            float xMove = Random.Range(-1, 1)* 0.2f;
            float yMove = Random.Range(-1, 1)*0.2f ;
            cam.transform.localPosition = new Vector3(pos.x, pos.y + yMove, pos.z + xMove);
            duration += Time.deltaTime;
            yield return null;
        }
        HandleCamMovement();
    }
        

    private void HandleCollision(HitDirection hitDirection) {
        if (hitDirection.Equals(HitDirection.SITE)) {
            move.setSiteHit(true);
            StartCoroutine(ShakeCam(0.2f));
            manager.setNearDeath(true);
        }
        else {
            manager.setPlayerEnum(PlayerStatEnum.DEAD);
            SceneManager.LoadScene("StartMenuScene");
        }
        Vibration.Vibrate(50);
    }
}
