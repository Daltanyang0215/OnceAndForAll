using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitaction
{
    [SerializeField] protected EnemyInfo _enemyInfo;
    protected Animator _animator;

    public Transform target;
    public NavMeshAgent navi;
    public Rigidbody rb;
    private Vector3 _targetpos;

    protected float _enemyHealth;
    private float _enemyMaxHealth;
    private int _enemyDamage = 1;
    protected bool _enemyIsDead = false;
    public bool IsDead => _enemyIsDead;

    private List<EnemyBuffBase> _enemyBuffs = new List<EnemyBuffBase>();
    public bool isImmortality;
    public float decrease;

    private AudioSource _audio;

    public virtual float EnemyHealth
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
                if (!isImmortality)
                {
                    Die();
                }
            }
        }
    }

    protected float _moveSpeed;
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
        _audio = GetComponent<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        // 몬스터 소환시 강화 적용
        GetComponent<SphereCollider>().enabled = true;
        navi.enabled = true;
        transform.localScale = Vector3.one;
        EnemyHealth = _enemyInfo.EnemyHealth * StatesEnforce.Instance.enemyHealthGain;
        _enemyIsDead = false;
        _moveSpeed = _enemyInfo.EnemySpeed * StatesEnforce.Instance.enemySpeedGain;
        MoveSpeed = _moveSpeed;
        isImmortality = false;
        decrease = 0;
    }

    protected virtual void Die()
    {
        if (_enemyIsDead) return;
        _enemyIsDead = true;
        MainGameManager.Instance.Money += (int)(_enemyInfo.Money * StatesEnforce.Instance.enemyMoneyGain);
        MainGameManager.Instance.currentEnemyCount--;
        MoveSpeed = 0;
        _animator.SetTrigger("DoDie");
        _audio.Play();
        GetComponent<SphereCollider>().enabled = false;
        OnActiveBuff(BuffStatus.Disable);
    }
    protected virtual void Update()
    {
        OnActiveBuff(BuffStatus.Update);
    }

    protected virtual void FixedUpdate()
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
            MainGameManager.Instance.currentEnemyCount--;

            ObjectPool.Instance.Spawn("BoomEffect", transform.position);
            
            PoolReturn();
        }
    }

    private void OnActiveBuff(BuffStatus status)
    {
        for (int i = _enemyBuffs.Count-1; i >= 0; i--)
        {
            _enemyBuffs[i].BuffActive(status); 
        }
    }
    private float OnHitBuff(float damage)
    {
        foreach (EnemyBuffBase buff in _enemyBuffs)
        {
            damage = buff.HitActive(damage);
        }
        return damage;
    }

    public void AddBuff(EnemyBuffBase buff)
    {
        if (_enemyBuffs.Contains(buff)) return;
        _enemyBuffs.Add(buff);
        buff.BuffActive(BuffStatus.Enable);
    }
    public void RemoveBuff(EnemyBuffBase buff)
    {
        if (_enemyBuffs.Contains(buff))
        {
            _enemyBuffs.Remove(buff);
        }
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

        damage = OnHitBuff(damage);

        if (damage>0)
        {
            _animator.SetBool("DoHit", true);
            EnemyHealth -= damage - (damage * 0.01f * decrease);
        }
    }

    // 애니메이션 종료 시 오브젝트 풀 리턴 용
    public void PoolReturn()
    {
        ObjectPool.Instance.Return(this.gameObject);
        MainGameManager.Instance.RoundEndCheck();
    }
}
