using UnityEngine;


[CreateAssetMenu(fileName = "EnemyBase", menuName = "Enemy/EnemyBase")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private int _enemyID;
    public int EnemyID => _enemyID;

    [SerializeField] private int _enemyHealth;
    public int EnemyHealth => _enemyHealth;

    [SerializeField] private float _enemySpeed;
    public float EnemySpeed => _enemySpeed;
    

}
