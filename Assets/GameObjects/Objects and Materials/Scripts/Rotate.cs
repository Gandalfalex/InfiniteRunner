using UnityEngine;

public class Rotate : MonoBehaviour
{

    
    void Update() {
        float rotation = Time.time - Time.realtimeSinceStartup;
        Debug.Log(rotation);
        transform.Rotate(new Vector3(0, 0, 1), rotation * Time.deltaTime * 20); 
    }
}
