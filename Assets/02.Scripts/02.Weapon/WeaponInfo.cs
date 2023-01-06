using UnityEngine;

public enum WeaponType
{
    Rifle,
    Shotgun,
    Sniper,
    Laser,
    Add
}
[CreateAssetMenu(fileName = "WeaponBase",menuName ="Weapon/WeaponBase")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private WeaponType _type;
    public WeaponType Type => _type;

    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _attackCool;
    public float AttackCool => _attackCool;
    
    [SerializeField] private float _reloadTime;
    public float ReloadTime => _reloadTime;
    
    [SerializeField] private float _hitCurcleError;
    public float HitCurcleError => _hitCurcleError;

    [SerializeField] private int _maxBullet;
    public int MaxBullet => _maxBullet;

    [SerializeField] private float _rebound;
    public float Rebound => _rebound;

}
