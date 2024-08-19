using UnityEngine;
using System.Collections.Generic;

public class TurretStats : MonoBehaviour
{
    public enum TurretType
    {
        Attack,
        Support
    }

    public enum AttributeType
    {
        Range,
        Attack,
        Speed
    }

    [System.Serializable]
    public class TurretAttribute
    {
        public AttributeType attributeType;
        public float value;
    }

    [System.Serializable]
    public class UpgradePath
    {
        public string pathName;
        public GameObject upgradePrefab;
    }

    public TurretType turretType = TurretType.Attack;
    public List<TurretAttribute> turretAttributes = new List<TurretAttribute>();
    public List<UpgradePath> upgradePaths = new List<UpgradePath>();

    [SerializeField]
    private float turretTurnSpeed = 10f;
    [SerializeField]
    private float turretDamage = 10f;
    [SerializeField]
    private float turretShotSpeed = 2f;
    [SerializeField]
    private int turretID;
    [SerializeField]
    private float turretRange = 3f;
    [SerializeField]
    private float turretDistanceToOtherTurrets = 1f;
    [SerializeField]
    private int turretLevel = 1;
    [SerializeField]
    private int turretCost = 100;

    private TurretAiming turretAiming;
    private TurretShooting turretShooting;
    private RangeIndicator rangeIndicator;

    private void Start()
    {
        if (turretType == TurretType.Attack)
        {
            turretAiming = GetComponent<TurretAiming>();
            if (turretAiming == null)
            {
                Debug.LogError("TurretAiming component missing on the object.");
                return;
            }
            turretAiming.turnSpeed = turretTurnSpeed;
            SetTurretRange(turretRange);

            turretShooting = GetComponent<TurretShooting>();
            if (turretShooting == null)
            {
                Debug.LogError("TurretShooting component missing on the object.");
                return;
            }
            turretShooting.shotSpeed = turretShotSpeed;
            turretShooting.turretDamage = turretDamage;
        }

        rangeIndicator = GetComponentInChildren<RangeIndicator>();
        if (rangeIndicator != null)
        {
            rangeIndicator.SetRadius(turretDistanceToOtherTurrets);
        }
        else
        {
            Debug.LogWarning("RangeIndicator component not found in child objects.");
        }
    }

    public GameObject GetNextUpgradePrefab(int pathIndex)
    {
        if (pathIndex >= 0 && pathIndex < upgradePaths.Count)
        {
            return upgradePaths[pathIndex].upgradePrefab;
        }
        return null;
    }

    public float GetTurretDamage()
    {
        return turretDamage;
    }

    public void SetTurretDamage(float newDamage)
    {
        turretDamage = newDamage;
    }

    public float GetTurretRange()
    {
        return turretRange;
    }

    public void SetTurretRange(float range)
    {
        turretRange = range;

        if (turretAiming != null)
        {
            turretAiming.SetRange(turretRange);
        }
    }

    public float GetTurretShotSpeed()
    {
        return turretShotSpeed;
    }

    public void SetTurretShotSpeed(float speed)
    {
        turretShotSpeed = speed;

        if (turretShooting != null)
        {
            turretShooting.shotSpeed = turretShotSpeed;
        }
    }

    public int GetTurretLevel()
    {
        return turretLevel;
    }

    public void SetTurretLevel(int level)
    {
        turretLevel = level;
    }

    public int GetTurretID()
    {
        return turretID;
    }

    public void SetTurretID(int newID)
    {
        turretID = newID;
    }

    public int GetTurretCost()
    {
        return turretCost;
    }

    public void SetTurretCost(int newCost)
    {
        turretCost = newCost;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
