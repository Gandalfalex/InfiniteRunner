using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelInformationHolder : MonoBehaviour, ObjectStatsInterface{
    public ObjectClass getObjectClass() {
        return ObjectClass.OBSTACLE;
    }

    public int getRecommendedListSize() {
        return 20;
    }

    public ItemTypes getType() {
        return ItemTypes.TUNNEL;
    }

}
