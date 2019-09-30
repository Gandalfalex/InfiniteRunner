using UnityEngine;

public class CoinInformationHolder : MonoBehaviour, CoinInterface{
    private int value;

    private void Awake() {
        value = Random.Range(1, 5);
    }

    public int getRecommendedListSize() {
        return 100;
    }

    public int getValue() {
        return value;
    }

    ItemTypes ObjectStatsInterface.getType() {
        return ItemTypes.COIN;
    }


    
}
