using UnityEngine;

public class CoinInformationHolder : MonoBehaviour, CoinInterface{
    public int getRecommendedListSize() {
        return 300;
    }

    public int getValue() {
        return 1; ;
    }

    ItemTypes ObjectStatsInterface.getType() {
        return ItemTypes.COIN;
    }
}
