using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEnemy : MonoBehaviour, IHitaction
{
    protected Animator _animator;

    protected float _enemyHealth = 5;
    protected bool _enemyIsDead = false;
    public bool IsDead => _enemyIsDead;

    public virtual float EnemyHealth
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;

            if (_enemyHealth <= 0)
            {
                Die();
            }
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 2 * Time.fixedDeltaTime);
    }
    public void OnHit(float damage)
    {
        if (damage > 0)
        {
            _animator.SetBool("DoHit", true);
            EnemyHealth -= damage;
        }
    }
    protected virtual void Die()
    {
        if (_enemyIsDead) return;
        _enemyIsDead = true;
        _animator.SetTrigger("DoDie");
        GetComponent<SphereCollider>().enabled = false;
    }

    public void PoolReturn()
    {
        Destroy(gameObject);
    }
}
