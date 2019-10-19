using UnityEngine;


[System.Serializable]
public sealed class CoinMaterialHolder  {
    [SerializeField]
    public Material[] materialsCoins;


    public static CoinMaterialHolder Instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<CoinMaterialHolder> lazy = new System.Lazy<CoinMaterialHolder>(() => new CoinMaterialHolder());


    public Material GetCoinMaterial(int i) {
       
        if(materialsCoins[i] != null) {
            return materialsCoins[i];
        }
        return null;
    }
}
