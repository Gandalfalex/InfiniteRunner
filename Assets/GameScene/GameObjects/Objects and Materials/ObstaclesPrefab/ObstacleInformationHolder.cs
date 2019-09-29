using UnityEngine;


public class ObstacleInformationHolder : MonoBehaviour, ObjectStatsInterface {
    public int getRecommendedListSize() {
        return 50;
    }

    // Start is called before the first frame update
    public ItemTypes getType() {
        return ItemTypes.OBSTACLE;
      }
}