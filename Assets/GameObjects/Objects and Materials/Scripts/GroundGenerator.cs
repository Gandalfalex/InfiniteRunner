using UnityEngine;

public class Groundgenerator {

    Vector3 obstacle_scale;


    private int noObstacle_x;
    private int obstacles_z_start;
    private int obstacles_z_end;

    private int coinSpawn;
    private int coinSpawn_position;
    private int noCoin = 5;

    public int size = 30;
    private float[] positions;
    private int lastPositionOfObject = 0;

    private bool generate;

    private ObjectPooler god;



    
    public Groundgenerator(Vector3 obstacle_scale, float[] positions, GameObject floor, GameObject obstacle, GameObject coin) {
        this.obstacle_scale = obstacle_scale;
        this.positions = positions;


        noObstacle_x = Random.Range(0, positions.Length);
        obstacles_z_start = Random.Range(6, 10);
        obstacles_z_end = obstacles_z_start + Random.Range(2, 6);


        coinSpawn = Random.Range(7, 11);
        coinSpawn_position = Random.Range(0, positions.Length);
       


        god = ObjectPooler.instance;
        god.setCoinGameObject(coin);
        god.setFloorGameObject(floor);
        god.setObstacleGameObject(obstacle);


    }

    
    public int getLastPosition() {
        return lastPositionOfObject;
    }
 
    public void generateLevel() { 
        int heigh = 1;
        int low = 0;
     
        for (int i = 0; i < size; i++) {

            int iScaler = i + lastPositionOfObject;
            foreach (float pos in positions) {
                handleObjects(ItemTypes.FLOOR, new Vector3(pos, low, iScaler * obstacle_scale.z));
            }
            if( i >= obstacles_z_start && i < obstacles_z_end) {
                foreach (float pos in positions) {
                    if (pos != positions[noObstacle_x]) {
                        handleObjects(ItemTypes.OBSTACLE, new Vector3(pos, heigh, iScaler * obstacle_scale.z));
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


    public void UpdateAtRuntime() {
        int heigh = 1;
        int low = 0;
        int offset = 2;


        foreach (float pos in positions) {
            handleObjects(ItemTypes.FLOOR, new Vector3(pos, low, lastPositionOfObject * obstacle_scale.z));
        }

        if (lastPositionOfObject >= obstacles_z_start && lastPositionOfObject < obstacles_z_end) {
            foreach (float pos in positions) {
                if (pos != positions[noObstacle_x]) {
                    handleObjects(ItemTypes.OBSTACLE, new Vector3(pos, heigh, lastPositionOfObject * obstacle_scale.z));
                }
            }
        }
        else if (lastPositionOfObject > obstacles_z_end) {
            obstacles_z_start = lastPositionOfObject + Random.Range(6, 10);
            obstacles_z_end = obstacles_z_start + Random.Range(2, 6);
            noObstacle_x = Random.Range(0, positions.Length);
            coinSpawn_position =Random.Range(0, positions.Length);
        }


        if(lastPositionOfObject + offset < obstacles_z_start) {

           handleObjects(ItemTypes.COIN, new Vector3(positions[coinSpawn_position] , heigh, lastPositionOfObject * obstacle_scale.z));
          
        }

        lastPositionOfObject ++;
    }



    public void handleObjects(ItemTypes item, Vector3 position) {

        GameObject temp = god.getOutOfObjectPool(item);
       
        if (temp != null) {
            temp.SetActive(true);
            temp.transform.position = position;
            temp.transform.rotation = Quaternion.identity;
        }
    }

}
