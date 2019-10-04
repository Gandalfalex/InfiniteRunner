using UnityEngine;


public class ObstacleInformationHolder : MonoBehaviour, ObstacleInterface {
    public Vector2 GetPositionAndHeight() {
        return new Vector2(.5f , 1.5f);
    }

    public ObjectClass getObjectClass() {
        return ObjectClass.OBSTACLE;
    }

    public int getRecommendedListSize() {
        return 50;
    }

    // Start is called before the first frame update
    public ItemTypes getType() {
        return ItemTypes.OBSTACLE;
      }


}