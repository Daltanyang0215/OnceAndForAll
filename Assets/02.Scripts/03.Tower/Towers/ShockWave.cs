using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : TowerRangeBase, IHitaction
{
    [SerializeField] private ParticleSystem _attackEffect;

    public void OnHit(float hitDamage)
    {
        Attack();
    }

    protected override void Attack()
    {
        _attackEffect.Play();
        cols = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        foreach (var col in cols)
        {
            if (col.TryGetComponent(out IHitaction enemy))
            {
                    enemy.OnHit(damage);
            }
        }
    }
}
