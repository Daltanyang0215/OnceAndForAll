using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfo _enemyInfo;
    private Animator _animator;

    private int _enemyHealth;
    private int _enemyHealthMax;

    private float _moveSpeed;

    private int EnemyHealth
    {
        get { return _enemyHealth; }
        set
        {
            _enemyHealth = value;

            if (_enemyHealth > _enemyHealthMax)
                _enemyHealth = _enemyHealthMax;

            if (_enemyHealth <= 0)
            {
                Die();
            }
        }
    }


    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _enemyHealthMax = _enemyInfo.EnemyHealth;
        EnemyHealth = _enemyHealthMax;
        MoveSpeed = _enemyInfo.EnemySpeed;
    }
    private void Die()
    {
        _animator.SetTrigger("Die");
        GetComponent<SphereCollider>().enabled = false;
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * -_moveSpeed*Time.fixedDeltaTime);

        // �����뼱 ����
        if(transform.position.z < -3f)
        {
            PoolReturn();
        }
    }

    // ������ ��
    public void Hit(int damage)
    {
        _animator.SetTrigger("Hit");
        EnemyHealth -= damage;
    }

    // �ִϸ��̼� ���� �� ������Ʈ Ǯ ���� ��
    public void PoolReturn()
    {
        ObjectPool.Instance.Return(this.gameObject);
        GetComponent<SphereCollider>().enabled = false;
    }


}
