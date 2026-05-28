using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Weapon_SO _data;
    

    public virtual void Shoot(RaySettings raySettings)
    {
        RaycastHit hit;
        Vector3 rayOrigin = raySettings.RayOrigin;
        Vector3 rayDirection = raySettings.RayDirection;


    }
}
