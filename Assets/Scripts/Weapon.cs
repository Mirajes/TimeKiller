using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Weapon_SO _Data;
    protected Transform _ShootPoint;

    public virtual void Shoot()
    {
        RaycastHit hit;
        Vector3 rayOrigin = _ShootPoint.transform.position;
        Vector3 rayDirection = _ShootPoint.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, _Data.Distance))
        {
            IDamageable damageMe = hit.transform.GetComponent<IDamageable>();
            if (damageMe != null)
            {
                damageMe.HandleHit(_Data.Damage);
            }
        }
    }
}
