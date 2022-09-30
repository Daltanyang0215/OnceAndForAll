using UnityEngine;

public enum TowerType
{
    Projectile,
    Boom,
    Range
}
[CreateAssetMenu(fileName = "TowerBase", menuName = "Tower/TowerBase")]
public class TowerInfo : ScriptableObject
{
    [SerializeField] private TowerType _type;
    public TowerType TowerType => _type;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _AttackCool;
    public float AttackCool => _AttackCool;

    [SerializeField] private float _AttackRange;
    public float AttackRange => _AttackRange;

    [SerializeField] private int _BuyCost;
    public int BuyCost => _BuyCost;

    [SerializeField] private int _SellCost;
    public int SellCost => _SellCost;
}
