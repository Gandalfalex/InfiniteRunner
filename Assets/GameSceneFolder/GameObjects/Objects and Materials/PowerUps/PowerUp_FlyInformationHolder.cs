using System;
using UnityEngine;

public class PowerUp_FlyInformationHolder : MonoBehaviour, PowerUpInterface
{
    public event EventHandler myCustomEvent;

    public float Duration() {
        return 10;
    }

    public ObjectClass GetObjectClass() {
        return ObjectClass.POWERUPS;
    }

    public PowerUp_Type GetPowerUp_Type() {
        return PowerUp_Type.FLY;
    }

    public int GetRecommendedListSize() {
        return 1;
    }

    public ItemType GetItemType() {
        return ItemType.FLY_UP;
    }
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Equals("Player")) {
            gameObject.SetActive(false);
        }
    }

    public void OnEvent() {
        throw new NotImplementedException();
    }
}
