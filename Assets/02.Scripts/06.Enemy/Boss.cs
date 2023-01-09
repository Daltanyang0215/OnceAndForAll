using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override float EnemyHealth
    {
        get => base.EnemyHealth;
        set
        {
            _enemyHealth = value;
            MainGameManager.Instance.currentEnemyCount = (int)_enemyHealth;
            if (_enemyHealth <= 0)
            {
                    Die();
            }

        }
    }

    protected override void OnEnable()
    {
        EnemyHealth = _enemyInfo.EnemyHealth;
        MainGameManager.Instance.currentEnemyCount = (int)_enemyHealth;
        
        _moveSpeed = _enemyInfo.EnemySpeed;
    }

    protected override void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * 2 * Time.fixedDeltaTime);
    }

    protected override void Die()
    {
        MainGameManager.Instance.LevelSuccess();   
    }
}
