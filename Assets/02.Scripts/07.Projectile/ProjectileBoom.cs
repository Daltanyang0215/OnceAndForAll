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
                    if (enemy is Enemy)
                    {
                        Enemy tmp = enemy as Enemy;
                        giveDebuff.SetOwner(tmp);
                        tmp.AddBuff(giveDebuff);
                    }
                }
            }
            if (isShowEffect)
            {
                GameObject effect = ObjectPool.Instance.Spawn(explosionEffect, tr.position);

                Color boomcolor = new Color(1,1,1,1);
                switch (attackElement)
                {
                    case Element.Normal:
                        boomcolor = new Color(1, 0.275f, 0, 1);
                        break;
                    case Element.Fire:
                        boomcolor = new Color(1, 0.275f, 0, 1);
                        break;
                    case Element.Ice:
                        boomcolor = new Color(0, 0.76f, 1, 1);
                        break;
                    case Element.Electricity:
                        boomcolor = new Color(1, 1f, 0.27f, 1);
                        break;
                    default:
                        break;
                }
                ParticleSystem.MainModule main = effect.GetComponent<ParticleSystem>().main;
                main.startColor = boomcolor;

                ObjectPool.Instance.Return(effect, 0.5f);
            }
            ObjectPool.Instance.Return(gameObject);
        }
        // 대상 레이어의 int 화 및 & 연산자를 이용한 포함여부 확인.
        // 0인지 확인하여 0이면 겹치는 레이어 아님. 0이 아니면 겹치는 레이어임

    }
}
