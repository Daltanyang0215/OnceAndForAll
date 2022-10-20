using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Projectile
{
    [SerializeField] private string _explosionEffect;
    [SerializeField] private bool _isShowEffect;

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
        // 대상 레이어의 int 화 및 & 연산자를 이용한 포함여부 확인.
        // 0인지 확인하여 0이면 겹치는 레이어 아님. 0이 아니면 겹치는 레이어임
        if(((1 << other.gameObject.layer)& touchLayer)!=0)
        {
            _isHit = true;
                ObjectPool.Instance.Return(gameObject, 1f);
        }

        if (_isShowEffect)
        {
            ObjectPool.Instance.Return(ObjectPool.Instance.Spawn(_explosionEffect, tr.position), 0.5f);
        }
    }
}
