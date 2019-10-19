using UnityEngine;

public class CoinColorChanger : MonoBehaviour
{
    
    public new MeshRenderer renderer;
    public Material[] materials;
   

    private void OnBecameVisible() {
        int i = gameObject.GetComponentInParent<ICoinInterface>().GetValue();
        if (materials[i] != null && i < materials.Length) {
            renderer.material = materials[i];
        }
    }

}
