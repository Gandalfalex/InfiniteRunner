using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelInformationHolder : MonoBehaviour, ObstacleInterface{
    public Vector2 GetPositionAndHeight() {
        return new Vector2(0, 3);
    }


    public ObjectClass getObjectClass() {
        return ObjectClass.OBSTACLE;
    }

    public int getRecommendedListSize() {
        return 20;
    }

    public ItemType getType() {
        return ItemType.TUNNEL;
    }

}
