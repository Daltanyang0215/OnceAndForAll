using UnityEngine;
[CreateAssetMenu(fileName = "TowerBase", menuName = "Tower/TowerBase")]
public class TowerInfo : ScriptableObject
{
    [SerializeField] private string _towerName;
    public string TowerName => _towerName;

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

    [SerializeField] private GameObject _towerPrefab;
    public GameObject TowerPrefab => _towerPrefab;
}
