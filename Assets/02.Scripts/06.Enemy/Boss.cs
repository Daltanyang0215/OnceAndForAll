using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private bool _isMove;

    private float _timer=2f;
    private Collider[] _cols;
    private float targetRange = 55f;

    public override float EnemyHealth
    {
        get => base.EnemyHealth;
        set
        {
            _enemyHealth = value;
            MainGameManager.Instance.currentEnemyCount = (int)_enemyHealth;
            if (_enemyHealth <= 0 && IsDead==false)
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
        _isMove = true;
    }

    protected override void Update()
    {
        if (IsDead) return;

        base.Update();
        if (_timer < 0)
        {
            _cols = Physics.OverlapSphere(transform.position, targetRange, 1<<LayerMask.NameToLayer("Tower"));
            if (_cols.Length > 3)
            {
                _isMove = false;
                if (Random.Range(0, 1f) > 0.5f)
                {
                    _animator.SetTrigger("JumpAttack");
                    _timer = 2f;
                    Invoke("TowerAttack", _timer*0.75f);
                }
                else
                {
                    _animator.SetTrigger("TowerAttack");
                    _timer = 3.3f;
                    Invoke("TowerAttack", _timer*0.75f);
                }
            }
            else
            {
                if (Physics.CheckSphere(transform.position, targetRange, 1 << LayerMask.NameToLayer("Goal")))
                {
                    _isMove = false;
                    _animator.SetTrigger("Attack");
                    _timer = 2.1f;
                }
                else
                {
                    _isMove = true;
                    _timer = 0.5f;
                }
            }
        }
        _timer -= Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        if (IsDead) return;

        if (_isMove)
            transform.Translate(Vector3.forward * _moveSpeed * 2 * Time.fixedDeltaTime);
    }

    protected override void Die()
    {
        _animator.SetTrigger("Die");
        _enemyIsDead = true;
        Invoke("GameEnd", 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }

    private void TowerAttack()
    {
        _cols = Physics.OverlapSphere(transform.position, targetRange, 1 << LayerMask.NameToLayer("Tower"));
        foreach (Collider col in _cols)
        {
            Destroy(col.gameObject);
        }
    }
    private void GoalAttack()
    {
        MainGameManager.Instance.Health -= 5;
    }

    private void GameEnd()
    {
        MainGameManager.Instance.LevelSuccess();
    }
}
