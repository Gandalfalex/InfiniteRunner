using System.Collections;
using System;
using UnityEngine;

public class ObjectEvent : MonoBehaviour
{
    public static event Action<ObjectEvent> pointOfInterestEvent;

    [SerializeField]
    private string eventName ;

    public string getEventName { get { return eventName; } }

    private void OnCollisionEnter(Collision other) {
        eventName += Time.time;
        pointOfInterestEvent?.Invoke(this);
    }
}
