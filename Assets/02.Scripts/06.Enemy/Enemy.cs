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
    private Vector3 _targetpos;

    private float _enemyHealth;
    private float _enemyMaxHealth;

    private float EnemyHealth
    {
        get { return _enemyHealth; }
        set
        {
            _enemyHealth = value;

            // �ִ�ü�� Ȯ��
            if (_enemyHealth > _enemyMaxHealth)
                _enemyMaxHealth = value;

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
            _navi.speed = value;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navi = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        // ���� ��ȯ�� ��ȭ ����
        GetComponent<SphereCollider>().enabled = true;
        EnemyHealth =_enemyInfo.EnemyHealth * StatesEnforce.Instance.enemyHealthGain;
        _moveSpeed = _enemyInfo.EnemySpeed*StatesEnforce.Instance.enemySpeedGain;
        MoveSpeed = _moveSpeed;
    }

    private void Die()
    {
        MainGameManager.Instance.Money += (int)(_enemyInfo.Money * StatesEnforce.Instance.enemyMoneyGain);
        _animator.SetTrigger("DoDie");
        GetComponent<SphereCollider>().enabled = false;
    }

    private void FixedUpdate()
    {
        _targetpos = target.position;
        _targetpos.x = transform.position.x;
        _navi.SetDestination(_targetpos);

        // �����뼱 ����
        if (transform.position.z < -3f)
        {
            MainGameManager.Instance.Health--;
            PoolReturn();
        }
    }

    // �ǰ� ��
    public bool Hit(float damage)
    {
        if (damage < 0) return false;
        _animator.SetTrigger("DoHit");
        EnemyHealth -= damage;

        if (EnemyHealth < 0) return true;
        return false;
    }

    public void Slow(bool isslow)
    {
        if (isslow)
        {
            MoveSpeed = _moveSpeed*0.5f;
        }
        else
        {
            MoveSpeed = _moveSpeed;
        }
    }

    // �ִϸ��̼� ���� �� ������Ʈ Ǯ ���� ��
    public void PoolReturn()
    {
        ObjectPool.Instance.Return(this.gameObject);
        MainGameManager.Instance.RoundEndCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null
            && other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("hit");
        }
    }
}
