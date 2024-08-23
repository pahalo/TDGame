using System;

[Serializable]
public class MapData
{
    public string mapName;
    public int health;
    public int money;

    public MapData(string mapName, int health, int money)
    {
        this.mapName = mapName;
        this.health = health;
        this.money = money;
    }
}
