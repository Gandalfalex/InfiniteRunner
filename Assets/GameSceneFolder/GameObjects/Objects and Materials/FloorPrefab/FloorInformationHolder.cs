
using UnityEngine;

public class FloorInformationHolder :MonoBehaviour, IObjectStatsInterface{
    public ObjectClassType GetObjectClass() {
        return ObjectClassType.NOT_INTERESSTING;
    }

    public int GetRecommendedListSize() {
        return 200;
    }

    public ItemType GetItemType() {
        return ItemType.FLOOR;
    }

}
