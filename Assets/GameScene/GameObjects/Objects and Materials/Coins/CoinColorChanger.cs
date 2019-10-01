using UnityEngine;

public class CoinColorChanger : MonoBehaviour
{
    
    public MeshRenderer renderer;
    public Material[] materials;
   

    private void OnBecameVisible() {
        int i = gameObject.GetComponentInParent<CoinInterface>().getValue();
        if (materials[i] != null) {
            Debug.Log(materials[i].color);
            renderer.material = materials[i];
        }
    }

}
