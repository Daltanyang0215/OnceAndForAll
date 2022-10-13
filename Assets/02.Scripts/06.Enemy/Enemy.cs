using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfo _enemyInfo;
    private Animator _animator;

    public Transform target;
    private NavMeshAgent _navi;

    private int _enemyHealth;

    private float _moveSpeed;

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


    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value;
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
        _animator.SetTrigger("Die");
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnEnable()
    {
        _navi.SetDestination(target.position);   
    }

    private void FixedUpdate()
    {
        //transform.Translate(transform.forward * -_moveSpeed*Time.fixedDeltaTime);
        
        // 마지노선 도착
        if(transform.position.z < -3f)
        {
            PoolReturn();
        }
    }

    // 데미지 용
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
    }


}
