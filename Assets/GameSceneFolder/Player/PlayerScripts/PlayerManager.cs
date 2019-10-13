
public sealed class PlayerManager{
    private PlayerManager() { }
        

    public PlayerStatEnum playerEnum;
    private int coins;
    public bool nearDeath = false;
    public float time;


    public static PlayerManager Instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<PlayerManager> lazy = new System.Lazy<PlayerManager>(() => new PlayerManager());



    public void setPlayerEnum(PlayerStatEnum player) {
        this.playerEnum = player;
    }

    public void setCoins(int coins) {
        this.coins = coins;
    }


    public void incCoins() {
        this.coins++;
    }


    public int getCoins() {
        return this.coins;
    }


    public PlayerStatEnum getPlayerEnum() {
        return this.playerEnum;
    }

    public void setNearDeath(bool death) {
        nearDeath = death;
    }
     
    public bool getNearDeath() {
        return nearDeath;
    }

    public int CoinMax() {
        return 80;
    }

}
