
using UnityEngine;

public class FloorInformationHolder :MonoBehaviour, ObjectStatsInterface{
    public ObjectClass getObjectClass() {
        return ObjectClass.NOT_INTERESSTING;
    }

    public int getRecommendedListSize() {
        return 200;
    }

    public ItemType getType() {
        return ItemType.FLOOR;
    }

}
