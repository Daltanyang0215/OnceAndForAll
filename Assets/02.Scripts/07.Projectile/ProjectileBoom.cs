using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoom : ProjectileBullet
{
    [SerializeField] private int _BoomRange;

    protected override void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == targetLayer ||
        ((1 << other.gameObject.layer) & touchLayer) != 0)
        {
            Collider[] Hits = Physics.OverlapSphere(tr.position, _BoomRange, targetLayer);

            foreach (var Hit in Hits)
            {
                if (Hit.gameObject.TryGetComponent(out IHitaction enemy))
                {
                    enemy.OnHit(damage);
                }
            }
            if (isShowEffect)
            {
                ObjectPool.Instance.Return(ObjectPool.Instance.Spawn(explosionEffect, tr.position), 0.5f);
            }
            ObjectPool.Instance.Return(gameObject);
        }
        // ��� ���̾��� int ȭ �� & �����ڸ� �̿��� ���Կ��� Ȯ��.
        // 0���� Ȯ���Ͽ� 0�̸� ��ġ�� ���̾� �ƴ�. 0�� �ƴϸ� ��ġ�� ���̾���

    }
}
