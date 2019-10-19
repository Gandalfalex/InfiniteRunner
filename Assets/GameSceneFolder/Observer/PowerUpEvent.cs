using UnityEngine;
using System;

public abstract class PowerUpEvent: MonoBehaviour {

    public static event Action<PowerUpEvent> PowerUp_Event;

    public void OnInvoceEvent() {
        Debug.Log("got invoked");
        PowerUp_Event?.Invoke(this);
    }
}
