using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelInformationHolder : MonoBehaviour, ObstacleInterface{
    public Vector2 GetPositionAndHeight() {
        return new Vector2(0, 0);
    }


    public ObjectClass GetObjectClass() {
        return ObjectClass.OBSTACLE;
    }

    public int GetRecommendedListSize() {
        return 20;
    }

    public ItemType GetItemType() {
        return ItemType.TUNNEL;
    }

}
