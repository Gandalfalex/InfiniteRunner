using System;
using UnityEngine;

public class PowerUp_FlyInformationHolder : PowerUpEvent, IPowerUpInterface
{
   
    

    public float Duration() {
        return 10;
    }

    public ObjectClassType GetObjectClass() {
        return ObjectClassType.POWERUPS;
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

    public void OnRaiseEvent() {
        OnInvoceEvent();
    }
}
