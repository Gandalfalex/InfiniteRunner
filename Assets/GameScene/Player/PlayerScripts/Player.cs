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


    private Rigidbody rb;
    private bool blocked;
    private Vector2 startPoint;
    private Vector2 directionRaw;
    private Vector2 endPoint;


    private void Awake(){
        rb = gameObject.GetComponent<Rigidbody>();
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
            else if (Input.GetKey(KeyCode.Space) && transform.position.y <= 1)
                rb.velocity = (new Vector3(0,  6, 0));
            else if (Input.touchCount > 0) {
                Touch touch = Input.touches[0];
                if (touch.phase.Equals(TouchPhase.Began) && !blocked) {
                    blocked = true;
                    startPoint = touch.position;
                }
                else if (touch.phase.Equals(TouchPhase.Ended) && blocked) {
                    blocked = true;
                    endPoint = touch.position;
                    //directionRaw = touch.position - startPoint;
                    Vector2 test = JoyStick.CalculateInput(startPoint, endPoint);
                    //float temp = directionRaw.x / Mathf.Abs(directionRaw.x);
                    move.firstMotion(transform.position, (test.x * floorTransform.localScale.x));
                    Debug.Log(test);
                    rb.velocity = (new Vector3(0, test.y * 6, 0));
                }
            }
        }
        else {
            manager.playerEnum = PlayerStatEnum.DEAD;
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
