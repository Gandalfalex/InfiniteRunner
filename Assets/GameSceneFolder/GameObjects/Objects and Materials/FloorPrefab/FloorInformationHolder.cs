
using UnityEngine;

public class FloorInformationHolder :MonoBehaviour, ObjectStatsInterface{
    public ObjectClass GetObjectClass() {
        return ObjectClass.NOT_INTERESSTING;
    }

    public int GetRecommendedListSize() {
        return 200;
    }

    public ItemType GetItemType() {
        return ItemType.FLOOR;
    }

}
