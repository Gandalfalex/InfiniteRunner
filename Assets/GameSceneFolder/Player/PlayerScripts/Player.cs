using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{

   
    public GameObject cam;
    public Vector3 offset;
    public Vector3 rotation;

    private PlayerManager manager = PlayerManager.Instance;

    private void Awake(){
        manager.setCoins(0);
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        FindObjectOfType<Soundmanager>().playAudio("background");
    }

    void Update(){
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        HandleCamMovement();
    }


    void OnCollisionEnter(Collision col) {
        ObjectClass itemType = col.gameObject.GetComponent<ObjectStatsInterface>().getObjectClass();
        if (itemType.Equals(ObjectClass.OBSTACLE)) {
            HandleCollision(WorkWithCollision(col.contacts[0].point, 
               col.gameObject.transform.position, col.gameObject.transform.localScale.x));
        }
    }


    public void HandleCamMovement() {
        float newXPosition = transform.position.x/10;
        float newZPosition = transform.position.z - offset.z;
        cam.transform.position = new Vector3(newXPosition, offset.y, newZPosition);
    }

            

    private void HandleCollision(HitDirection hitDirection) {
        if (hitDirection.Equals(HitDirection.SITE)) {
            manager.setNearDeath(true);
            StartCoroutine(ShakeCam(0.2f));
        }
        else {
            manager.setPlayerEnum(PlayerStatEnum.DEAD);
            SceneManager.LoadScene("StartMenuScene");
        }
        Vibration.Vibrate(50);
    }

    private IEnumerator ShakeCam(float time) {

        float duration = 0f;
        while (duration < time) {
            Vector3 pos = cam.transform.position;
            float xMove = Random.Range(-1, 1) * 0.2f;
            float yMove = Random.Range(-1, 1) * 0.2f;
            cam.transform.localPosition = new Vector3(pos.x, pos.y + yMove, pos.z + xMove);
            duration += Time.deltaTime;
            yield return null;
        }
        HandleCamMovement();
    }


    public HitDirection WorkWithCollision(Vector3 collision, Vector3 obstacle, float localScale_x) {
        Vector3 temp = (collision - transform.position);
        if (temp.x != 0) {
            return HitDirection.SITE;
        }
        else if (Mathf.Abs(collision.x) <= (obstacle.x) && Mathf.Abs(collision.x) >= (obstacle.x - localScale_x)) {
            return HitDirection.FRONT;
        }
        return HitDirection.UNKNOWN;
    }
}
