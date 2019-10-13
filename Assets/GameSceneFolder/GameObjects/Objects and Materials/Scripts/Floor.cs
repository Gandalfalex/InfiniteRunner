using UnityEngine;

public class Floor: MonoBehaviour {

  
    private Groundgenerator groundgenerator;
    
    [SerializeField]
    private GameObject[] gameObjects;
    [SerializeField]
    private Transform playerPosition;

    private Vector3 floorScale;
   
    private void Awake() {
        groundgenerator = new Groundgenerator(gameObjects);
        foreach(GameObject game in gameObjects) {
            if (game.GetComponent<ObjectStatsInterface>().getType().Equals(ItemType.FLOOR)) {
                floorScale = game.transform.localScale;
            }
        }
        groundgenerator.GenerateLevel();
    }


    /* if the distance between the player and the last active object is less then 10
     * then !stopGenerating 
     * else start generating the new level
     */
   void Update() {
        if (groundgenerator.GetLastPosition() * floorScale.z - transform.position.z < 200){
            groundgenerator.UpdateAtRuntime();  
        }
    }

    public void CleanPooler() {
        groundgenerator.DestroyObjectPooler();
    }

}