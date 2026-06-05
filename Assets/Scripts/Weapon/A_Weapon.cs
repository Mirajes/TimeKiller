using UnityEngine;

public abstract class A_Weapon : MonoBehaviour
{
    [SerializeField] protected SO_Weapon _Data;
    [SerializeField] protected Transform _ShootPoint;

    public virtual void Shoot()
    {
        GameManager.TimeUse?.Invoke(_Data.TimeCostPerBullet);

        //Vector3 rayOrigin = _ShootPoint.position;
        //Vector3 rayDirection = _ShootPoint.forward;

        Vector3 screenPoint = new Vector3(
            Screen.width / 2,
            Screen.height / 2
            );
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, _Data.Distance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 5);

            IDamageable damageMe = hit.transform.GetComponent<IDamageable>();
            damageMe?.HandleHit(_Data.Damage);
        }
    }
    
}