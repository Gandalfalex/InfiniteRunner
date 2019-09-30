using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        PlayerPrefs.DeleteAll();
        ObjectEvent.pointOfInterestEvent += ObjectEvent_PointOfInterestEvent;
    }

    private void Unregister() {
        ObjectEvent.pointOfInterestEvent -= ObjectEvent_PointOfInterestEvent;
    }

    private void ObjectEvent_PointOfInterestEvent(ObjectEvent oEvent) {
        string key = "you hit: " + oEvent.getEventName; 
        if(PlayerPrefs.GetInt(key) == 1) {
            return;
        }
        PlayerPrefs.SetInt(key, 1);
        Debug.Log(key);
    }
}
