
using UnityEngine;

public class FloorInformationHolder :MonoBehaviour, ObjectStatsInterface{
    public int getRecommendedListSize() {
        return 200;
    }

    public  ItemTypes getType() {
        return ItemTypes.FLOOR;
    }

}
