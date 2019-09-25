using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionEnter : MonoBehaviour{
   
    void OnCollisionEnter(Collision collision) {

        
        float contactPoint_z = collision.contacts[0].point.z;
        Vector3 test = (collision.contacts[0].point - transform.position);
        if (test.z != 0) {
            Debug.Log("site hit");
        }

        GameObject game = collision.gameObject;
        if (pointBetweenBoarders(contactPoint_z, game.transform.localScale, game.transform.position)) {
            
            PlayerManager manager = PlayerManager.Instance;
            manager.setPlayerEnum(PlayerEnum.DEAD);
        }
        
        
    }



    public bool pointBetweenBoarders(float contactPoint_z, Vector3 scale, Vector3 pos) {

        if (Mathf.Abs(contactPoint_z) <= (pos.z) && Mathf.Abs(contactPoint_z) >= (pos.z - scale.z)) {
            return true;
        }
        return false;
    }
}

