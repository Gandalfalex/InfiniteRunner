using UnityEngine;

public class CoinInformationHolder : MonoBehaviour, CoinInterface{
    private int value = 1;

   
    public int GetRecommendedListSize() {
        return 50;
    }

    public int GetValue() {
        return value;
    }

    ItemType ObjectStatsInterface.GetItemType() {
        return ItemType.COIN;
    }
    public void SetValue(int newValue) {
        value = newValue;
    }

    public ObjectClass GetObjectClass() {
        return ObjectClass.NOT_INTERESSTING;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            PlayerManager.Instance.incCoins();
            gameObject.SetActive(false);
            SoundManagerAccess.PlayAudioWithPitch("collect", PlayerManager.Instance.getCoins()% PlayerManager.Instance.CoinMax());
        }
    }
   

}
