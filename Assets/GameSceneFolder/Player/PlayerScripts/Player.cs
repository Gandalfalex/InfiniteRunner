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
    }

    private void Start() {
        SoundManagerAccess.PlayRandomAudioByType(SoundType.MAINTHEME);
    }

    void Update(){
        cam.transform.position = offset;
        cam.transform.rotation = Quaternion.Euler(rotation);
        HandleCamMovement();
    }


    void OnCollisionEnter(Collision col) {
        ObjectClass itemType = col.gameObject.GetComponent<ObjectStatsInterface>().GetObjectClass();
        if (itemType.Equals(ObjectClass.OBSTACLE)) {
            HandleCollision( TestCollision(transform.position, col.contacts[0].point, transform.localScale));
        }
    }
    private HitDirection TestCollision(Vector3 playerPosition, Vector3 collision, Vector3 playerScale) {
        Vector3 temp = (collision - playerPosition).normalized;
        
        if (temp.z == 1)
            return HitDirection.FRONT;
        if (Mathf.Abs(temp.x) == 1)
            return HitDirection.SITE;
        else return HitDirection.UNKNOWN;
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
        else {//if (hitDirection.Equals(HitDirection.FRONT) || hitDirection.Equals(HitDirection.UNKNOWN)) {
            manager.setPlayerEnum(PlayerStatEnum.DEAD);
            SceneManager.LoadScene("StartMenuScene");
            SoundManagerAccess.StopAudioByType(SoundType.MAINTHEME);
            SoundManagerAccess.PlayAudio("death");
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
}
