using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Projectile
{
    [SerializeField] protected string explosionEffect;
    [SerializeField] protected bool isShowEffect;

    [SerializeField] private int _attackCount;

    public void SetUp(Transform target, float speed, float damage, bool isGuided, LayerMask touchLayer, LayerMask targetLayer, Element element = Element.Normal, int attackCount = 1)
    {
        base.SetUp(target, speed, damage, isGuided, touchLayer, targetLayer, element);
        _attackCount = attackCount;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == targetLayer)
        {
            if (other.gameObject.TryGetComponent(out IHitaction enemy)
                && _attackCount > 0)
            {
                enemy.OnHit(damage);
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

        if (isShowEffect)
        {
            ObjectPool.Instance.Return(ObjectPool.Instance.Spawn(explosionEffect, tr.position), 0.1f);
        }
    }
}
