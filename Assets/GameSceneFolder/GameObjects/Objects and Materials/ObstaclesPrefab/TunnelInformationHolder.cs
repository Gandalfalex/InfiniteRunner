using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelInformationHolder : MonoBehaviour, IObstacleInterface{
    public Vector2 GetPositionAndHeight() {
        return new Vector2(0, 0);
    }


    public ObjectClassType GetObjectClass() {
        return ObjectClassType.OBSTACLE;
    }

    public int GetRecommendedListSize() {
        return 20;
    }

    public ItemType GetItemType() {
        return ItemType.TUNNEL;
    }

}
