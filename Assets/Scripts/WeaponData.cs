using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _distance = float.PositiveInfinity;
    [SerializeField] private float _timeCostPerBullet;

    public string Name => _name;
    public float Damage => _damage;
    public float Distance => _distance;
    public float TimeCostPerBullet => _timeCostPerBullet;
}
