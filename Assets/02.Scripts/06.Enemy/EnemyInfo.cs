using UnityEngine;


[CreateAssetMenu(fileName = "EnemyBase", menuName = "Enemy/EnemyBase")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private int _enemyID;
    public int EnemyID => _enemyID;

    [SerializeField] private int _enemyHealth;
    public int EnemyHealth => (int)(_enemyHealth * StatesEnforce.enemyHealthGain);

    [SerializeField] private float _enemySpeed;
    public float EnemySpeed => (int)(_enemySpeed * StatesEnforce.enemySpeedGain);

    [SerializeField] private int _money;
    public int Money => _money;
}
