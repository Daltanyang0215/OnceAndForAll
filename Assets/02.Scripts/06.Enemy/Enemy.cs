using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitaction
{
    [SerializeField] private EnemyInfo _enemyInfo;
    private Animator _animator;

    public Transform target;
    public NavMeshAgent navi;
    public Rigidbody rb;
    private Vector3 _targetpos;

    private float _enemyHealth;
    private float _enemyMaxHealth;
    private int _enemyDamage = 1;

    private List<EnemyBuffBase> _enemyBuffs = new List<EnemyBuffBase>();
    public bool isImmortality;
    public float decrease;
    public bool ignoringHits;

    public float EnemyHealth
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;

            // 최대체력 확장
            if (_enemyHealth > _enemyMaxHealth)
                _enemyMaxHealth = value;

            if (_enemyHealth <= 0)
            {
                if (isImmortality)
                {
                    _enemyHealth = 1;
                }
                else
                {
                    Die();
                }
            }
        }
    }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            navi.speed = value;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // 몬스터 소환시 강화 적용
        GetComponent<SphereCollider>().enabled = true;
        navi.enabled = true;
        transform.localScale = Vector3.one;
        EnemyHealth = _enemyInfo.EnemyHealth * StatesEnforce.Instance.enemyHealthGain;
        _moveSpeed = _enemyInfo.EnemySpeed * StatesEnforce.Instance.enemySpeedGain;
        MoveSpeed = _moveSpeed;
        isImmortality = false;
        decrease = 0;
    }

    private void Die()
    {
        MainGameManager.Instance.Money += (int)(_enemyInfo.Money * StatesEnforce.Instance.enemyMoneyGain);
        MainGameManager.Instance.currentEnemyCount--;
        MoveSpeed = 0;
        _animator.SetTrigger("DoDie");
        GetComponent<SphereCollider>().enabled = false;
        OnActiveBuff(BuffStatus.Disable);
    }
    private void Update()
    {
        if(EnemyHealth >0)
        OnActiveBuff(BuffStatus.Update);
    }

    private void FixedUpdate()
    {
        if (navi.enabled)
        {
            _targetpos = target.position;
            _targetpos.x = transform.position.x;
            navi.SetDestination(_targetpos);
        }
        else
        {
            transform.Translate(Vector3.forward * MoveSpeed * 2 * Time.fixedDeltaTime);
        }


        // 마지노선 도착
        if (transform.position.z < -3f)
        {
            MainGameManager.Instance.Health -= _enemyDamage;
            PoolReturn();
        }
    }

    private void OnActiveBuff(BuffStatus status)
    {
        foreach (EnemyBuffBase buff in _enemyBuffs)
        {
            buff.BuffActive(status);
        }
    }

    public void AddBuff(EnemyBuffBase buff)
    {
        _enemyBuffs.Add(buff);
        buff.BuffActive(BuffStatus.Enable);
    }
    public bool CheckBuff(Type checkbuff)
    {
        foreach (EnemyBuffBase buff in _enemyBuffs)
        {
            if (buff.GetType() == checkbuff)
                return true;
        }
        return false;
    }
    // 피격 용
    public void OnHit(float damage)
    {
        OnActiveBuff(BuffStatus.Hit);

        if (!ignoringHits)
        {
            _animator.SetBool("DoHit", true);
            EnemyHealth -= damage - (damage * 0.01f * decrease);
        }
        ignoringHits = false;
    }

    // 애니메이션 종료 시 오브젝트 풀 리턴 용
    public void PoolReturn()
    {
        ObjectPool.Instance.Return(this.gameObject);
        MainGameManager.Instance.RoundEndCheck();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            navi.enabled = true;
            rb.isKinematic = true;
        }
    }
}
