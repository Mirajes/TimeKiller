using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_SO", menuName = "Scriptable Objects/Weapon_SO")]
public class SO_Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _distance = float.PositiveInfinity;
    [SerializeField] private float _timeCostPerBullet;
    [SerializeField] private LayerMask _hitLayers;

    public string Name => _name;
    public float Damage => _damage;
    public float Distance => _distance;
    public float TimeCostPerBullet => _timeCostPerBullet;
    public LayerMask HitLayers => _hitLayers;
}
