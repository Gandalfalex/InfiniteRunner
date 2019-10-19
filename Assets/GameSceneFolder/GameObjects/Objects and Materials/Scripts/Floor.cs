using UnityEngine;
using UnityEngine.UI;

public class Floor: MonoBehaviour {

  
    private Groundgenerator groundgenerator;
    
    [SerializeField]
    public GameObject[] gameObjects;
    [SerializeField]
    public Transform playerPosition;
    public Text text;

    private Vector3 floorScale;
   
    private void Awake() {
        groundgenerator = new Groundgenerator(gameObjects);
        foreach(GameObject game in gameObjects) {
            if (game.GetComponent<IObjectStatsInterface>().GetItemType().Equals(ItemType.FLOOR)) {
                floorScale = game.transform.localScale;
            }
        }
        groundgenerator.GenerateLevel();
    }



    /* if the distance between the player and the last active object is less then 10
     * then !stopGenerating 
     * else start generating the new level
     */
    private void Update() {
       
        if (groundgenerator.GetLastPosition() * floorScale.z - transform.position.z < 200) {
            text.text = Mathf.RoundToInt(playerPosition.position.z).ToString();
            groundgenerator.UpdateAtRuntime();
            if (groundgenerator.raiseEvent) {
                PowerUpEvent.PowerUp_Event += PowerUPEvent_RegisterNewEvent;
            }
        }
    }

    public void CleanPooler() {
        groundgenerator.DestroyObjectPooler();
    }


    private void Unregister() {
        PowerUpEvent.PowerUp_Event -= PowerUPEvent_RegisterNewEvent;
    }

    private void PowerUPEvent_RegisterNewEvent(PowerUpEvent oEvent) {
        IPowerUpInterface temp = (oEvent.GetComponentInChildren<IPowerUpInterface>());
        Debug.Log(temp.GetType() + "   " + temp.GetPowerUp_Type());

        Unregister();
    }
}