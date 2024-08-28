using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurretData
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public int turretIndex;
    public int turretID;

    public TurretData(Vector3 position, Quaternion rotation, int turretIndex, int turretID)
    {
        this.position = new SerializableVector3(position);
        this.rotation = new SerializableQuaternion(rotation);
        this.turretIndex = turretIndex;
        this.turretID = turretID;
    }
}

[Serializable]
public class MapData
{
    public string mapName;
    public int health;
    public int money;
    public List<TurretData> turrets;
    public int currentWaveIndex;

    public MapData(string mapName, int health, int money, List<TurretData> turrets, int currentWaveIndex)
    {
        this.mapName = mapName;
        this.health = health;
        this.money = money;
        if (turrets == null)
        {
            this.turrets = new List<TurretData>();
        }       
        else
        {
            this.turrets = turrets;
        }
        this.currentWaveIndex = currentWaveIndex;
    }
}
[Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

[Serializable]
public class SerializableQuaternion
{
    public float x, y, z, w;

    public SerializableQuaternion(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}