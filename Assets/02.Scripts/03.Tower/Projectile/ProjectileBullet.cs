using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Projectile
{
    [SerializeField] private ParticleSystem _explosionEffect;

    [SerializeField] private int _attackCount;

    protected override void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == targetLayer)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy)
                && _attackCount > 0)
            {
                enemy.Hit(damage);
                _attackCount--;
            }
            //GameObject effect = ObjectPool.Instance.Spawn("Effect", tr.position, Quaternion.LookRotation(tr.position - other.transform.position));
            //ObjectPool.Instance.Return(effect, _explosionEffect.main.duration + _explosionEffect.main.startLifetime.constantMax);
        }
        if(1 << other.gameObject.layer == touchLayer)
        {
            _isHit = true;
                ObjectPool.Instance.Return(gameObject, 1f);
        }
    }
}
