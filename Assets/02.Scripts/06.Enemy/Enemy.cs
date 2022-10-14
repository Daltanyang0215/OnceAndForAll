using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfo _enemyInfo;
    private Animator _animator;

    private NavMeshAgent _navi;
    public Transform target;
    private Vector3 _targetpos;

    private int _enemyHealth;
    private int EnemyHealth
    {
        get { return _enemyHealth; }
        set
        {
            _enemyHealth = value;

            if (_enemyHealth > _enemyInfo.EnemyHealth)
                _enemyHealth = _enemyInfo.EnemyHealth;

            if (_enemyHealth <= 0)
            {
                Die();
            }
        }
    }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set
        {
            _moveSpeed = value;
            _navi.speed = _moveSpeed;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navi = GetComponent<NavMeshAgent>();

        EnemyHealth = _enemyInfo.EnemyHealth;
        MoveSpeed = _enemyInfo.EnemySpeed;
    }
    private void Die()
    {
        MainGameManager.Instance.Money += _enemyInfo.Money;
        _animator.SetTrigger("Die");
        GetComponent<SphereCollider>().enabled = false;
    }

    private void FixedUpdate()
    {
        //transform.Translate(transform.forward * -_moveSpeed*Time.fixedDeltaTime);
        _targetpos = target.position;
        _targetpos.x = transform.position.x;
        _navi.SetDestination(_targetpos);

        // 마지노선 도착
        if (transform.position.z < -3f)
        {
            MainGameManager.Instance.Health--;
            PoolReturn();
        }
    }

    // 피격 용
    public void Hit(int damage)
    {
        _animator.SetTrigger("Hit");
        EnemyHealth -= damage;
    }

    // 애니메이션 종료 시 오브젝트 풀 리턴 용
    public void PoolReturn()
    {
        ObjectPool.Instance.Return(this.gameObject);
        GetComponent<SphereCollider>().enabled = false;
        MainGameManager.Instance.LevelEndCheck();
    }


}
