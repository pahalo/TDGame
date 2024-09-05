using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    public void SellTurret(GameObject turret, int sellAmount)
    {
        GameManager.Instance.AddMoney(sellAmount);
        Destroy(turret);
    }
}
