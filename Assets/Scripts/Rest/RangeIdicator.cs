using UnityEngine;

// This Script will set the size of radius which surronds the turret (No build zone)
public class RangeIndicator : MonoBehaviour
{
    public void SetRadius(float radius)
    {
        float diameter = radius * 2f;
        transform.localScale = new Vector3(diameter, diameter, 1f);
    }
}
