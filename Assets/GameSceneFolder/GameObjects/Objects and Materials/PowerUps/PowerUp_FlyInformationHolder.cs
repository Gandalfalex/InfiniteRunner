using UnityEngine;

public class PowerUp_FlyInformationHolder : MonoBehaviour, PowerUpInterface
{
    public float Duration() {
        return 10;
    }

    public ObjectClass getObjectClass() {
        return ObjectClass.POWERUPS;
    }

    public PowerUp_Type GetPowerUp_Type() {
        return PowerUp_Type.FLY;
    }

    public int getRecommendedListSize() {
        return 1;
    }

    public ItemType getType() {
        return ItemType.FLY_UP;
    }
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Equals("Player")) {
            Debug.Log("hit Fly");
            gameObject.SetActive(false);
        }
    }

}
