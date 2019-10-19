using UnityEngine;


public class ObstacleInformationHolder : MonoBehaviour, IObstacleInterface {
    public Vector2 GetPositionAndHeight() {
        return new Vector2(.5f , 1.5f);
    }

    public ObjectClassType GetObjectClass() {
        return ObjectClassType.OBSTACLE;
    }

    public int GetRecommendedListSize() {
        return 50;
    }

    // Start is called before the first frame update
    public ItemType GetItemType() {
        return ItemType.OBSTACLE;
      }


}