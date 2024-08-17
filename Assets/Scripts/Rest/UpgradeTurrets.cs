using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{

    public void UpgradeTurret(TurretStats turretStats) {
        turretStats.SetTurretRange(4);
        Debug.Log("Turret Upgraded");
    }
}
