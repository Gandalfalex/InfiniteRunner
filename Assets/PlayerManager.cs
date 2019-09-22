public sealed class PlayerManager{
    private PlayerManager() { }


    public PlayerEnum playerEnum;
    public int coins;


    public static PlayerManager Instance {
        get { return lazy.Value; }
    }

    private static readonly System.Lazy<PlayerManager> lazy = new System.Lazy<PlayerManager>(() => new PlayerManager());



    public void setPlayerEnum(PlayerEnum player) {
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


    public PlayerEnum getPlayerEnum() {
        return this.playerEnum;
    }
}
