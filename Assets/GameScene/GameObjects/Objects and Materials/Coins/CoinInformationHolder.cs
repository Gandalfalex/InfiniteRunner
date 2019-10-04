using UnityEngine;

public class CoinInformationHolder : MonoBehaviour, CoinInterface{
    private int value = 1;

   
    public int getRecommendedListSize() {
        return 50;
    }

    public int getValue() {
        return value;
    }

    ItemTypes ObjectStatsInterface.getType() {
        return ItemTypes.COIN;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            PlayerManager.Instance.incCoins();
            gameObject.SetActive(false);
            FindObjectOfType<Soundmanager>().playAudio("Hit", PlayerManager.Instance.getCoins());
            value = 0;
        }
    }

    public void setValue(int newValue) {
        value = newValue;
    }

    public ObjectClass getObjectClass() {
        return ObjectClass.NOT_INTERESSTING;
    }
}
