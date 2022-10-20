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
        // ��� ���̾��� int ȭ �� & �����ڸ� �̿��� ���Կ��� Ȯ��.
        // 0���� Ȯ���Ͽ� 0�̸� ��ġ�� ���̾� �ƴ�. 0�� �ƴϸ� ��ġ�� ���̾���
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
