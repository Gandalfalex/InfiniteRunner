using UnityEngine;
using UnityEngine.UI;

public class Floor: MonoBehaviour {

  
    private Groundgenerator groundgenerator;
    
    [SerializeField]
    private GameObject[] gameObjects;
    [SerializeField]
    private Transform playerPosition;
    public Text text;

    private Vector3 floorScale;
   
    private void Awake() {
        groundgenerator = new Groundgenerator(gameObjects);
        foreach(GameObject game in gameObjects) {
            if (game.GetComponent<ObjectStatsInterface>().GetItemType().Equals(ItemType.FLOOR)) {
                floorScale = game.transform.localScale;
            }
        }
        groundgenerator.GenerateLevel();
        text.text = "GenerateLevel had no error, Where is the error????";
       
        
    }



    /* if the distance between the player and the last active object is less then 10
     * then !stopGenerating 
     * else start generating the new level
     */
    private void Update() {
       
        if (groundgenerator.GetLastPosition() * floorScale.z - transform.position.z < 200) {
            text.text = groundgenerator.SomeShit().ToString();
            groundgenerator.UpdateAtRuntime();
        }
    }

    public void CleanPooler() {
        groundgenerator.DestroyObjectPooler();
    }

}