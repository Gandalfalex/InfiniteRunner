using UnityEngine;



/*this class handles the leveldesign and generation.
 
 */
public class Groundgenerator {

    Vector3 floor_Scale;


    private int noObstacle_x;
    private int obstacles_z_start;
    private int obstacles_z_end;

    private int coinSpawn = 0;
    private int coinSpawn_position;
   // private int noCoin = 5;

    public int size = 10;
    private float[] positions;
    private int lastPositionOfObject = 0;

    private bool generate;

    private ObjectPooler god;



    /*Sets up random vars to generate the level
     */
    public Groundgenerator(GameObject[] gameObjects) {

        foreach (GameObject gObject in gameObjects) {
            if (gObject.GetComponent<ObjectStatsInterface>().getType().Equals(ItemTypes.FLOOR)) {
                floor_Scale = gObject.transform.localScale;
                positions = fillPositionsArray(floor_Scale.x);   
            }
        }
        god = ObjectPooler.instance;
        god.setGameObjects(gameObjects);

        noObstacle_x = Random.Range(0, positions.Length);
        obstacles_z_start = Random.Range(6, 10);
        obstacles_z_end = obstacles_z_start + Random.Range(2, 6);

        coinSpawn = Random.Range(7, 11);
        coinSpawn_position = Random.Range(0, positions.Length);
    }

    
    public int getLastPosition() {
        return lastPositionOfObject;
    }
 

    /*Generates the first part. The rest will be generated at run time
     */
    public void generateLevel() { 
        int heigh = 1;
        int low = 0;
     
        for (int i = 0; i < size; i++) {

            int iScaler = i + lastPositionOfObject;
            
            foreach (float pos in positions) {
                handleObjects(ItemTypes.FLOOR, new Vector3(pos, low, iScaler * floor_Scale.z));
            }
           
            if( i >= obstacles_z_start && i < obstacles_z_end) {
               
                foreach (float pos in positions) {
                    if (pos != positions[noObstacle_x]) {
                        handleObjects(ItemTypes.OBSTACLE, new Vector3(pos, heigh, iScaler * floor_Scale.z));
                    }
                }
            }
            else if(i > obstacles_z_end) {
                obstacles_z_start = i +  Random.Range(6, 10);
                obstacles_z_end = obstacles_z_start + Random.Range(2, 6);
                noObstacle_x = Random.Range(0, positions.Length);
            }
        }
        lastPositionOfObject = size;
    }



    /*based on their position, this method only creates a small part of the map
     */
    public void UpdateAtRuntime() {
        int heigh = 1;
        int tunnelHight = 3;
        int low = 0;
        int offset = 2;

        foreach (float pos in positions) {
            handleObjects(ItemTypes.FLOOR, new Vector3(pos, low, lastPositionOfObject * floor_Scale.z));
        }
        
        if (lastPositionOfObject >= obstacles_z_start && lastPositionOfObject < obstacles_z_end) {
            if (positions[noObstacle_x] == 0) {
                handleObjects(ItemTypes.TUNNEL, new Vector3(0, tunnelHight, lastPositionOfObject * floor_Scale.z));
            }
            else {
                foreach (float pos in positions) {

                    if (pos != positions[noObstacle_x]) {
                        handleObjects(ItemTypes.OBSTACLE, new Vector3(pos, heigh, lastPositionOfObject * floor_Scale.z));
                    }
                }
            }
        }
        else if (lastPositionOfObject > obstacles_z_end) {
            obstacles_z_start = lastPositionOfObject + Random.Range(6, 10);
            obstacles_z_end = obstacles_z_start + Random.Range(2, 6);
            noObstacle_x = Random.Range(0, positions.Length);
            coinSpawn_position = Random.Range(0, positions.Length);
        }


        if(lastPositionOfObject + offset < obstacles_z_start) {
           handleObjects(ItemTypes.COIN, new Vector3(positions[coinSpawn_position] , heigh, lastPositionOfObject * floor_Scale.z));
        }

        lastPositionOfObject ++;
    }


    /* activates new Prefabs, sets them active
     */
    public void handleObjects(ItemTypes item, Vector3 position) {
        GameObject temp = god.getOutOfObjectPool(item);
        if (temp != null && !item.Equals(ItemTypes.COIN)) {
            temp.SetActive(true);
            temp.transform.position = position;
            temp.transform.rotation = Quaternion.identity;
        }
        else if(temp != null && item.Equals(ItemTypes.COIN)) {
            temp.SetActive(true);
            temp.transform.position = position;
            temp.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    /* sets the objectpooler to default
     */
    public void destroyObjectPooler() {
        god.kill();
    }


    private float[] fillPositionsArray(float localScale_x) {
        float[] positions = new float[3];
        int pos = 0;
        for (float depth = -localScale_x; depth <= localScale_x; depth += localScale_x) {
            positions[pos] = depth;
            pos++;
        }
        return positions;
    }
}
