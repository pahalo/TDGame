using System;

[Serializable]
public class PlayerData
{
    public int money;
    public int health;

    public PlayerData(GameManager gameManager)
    {
        money = gameManager.PlayerMoney;
        health = gameManager.PlayersCurrentHealth;
    }
}
