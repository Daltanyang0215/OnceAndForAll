using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : EnemyBuffBase
{
    private float burningtime;
    private float timer;
    private float damage;
    private GameObject _prefab;

    public Burning(Enemy owner, float burningtime, float damage) : base(owner)
    {
        this.burningtime = burningtime;
        this.damage = damage; 
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                _prefab = ObjectPool.Instance.Spawn("Burning", owner.transform.position,owner.transform);
                ParticleSystem.ShapeModule shape = _prefab.GetComponent<ParticleSystem>().shape;
                shape.skinnedMeshRenderer = owner.GetComponentInChildren<SkinnedMeshRenderer>();
                timer = burningtime;
                break;
            case BuffStatus.Update:
                timer -= Time.deltaTime;
                owner.OnHit(damage * Time.deltaTime);
                if (timer <= 0 || owner.IsDead)
                {
                    owner.RemoveBuff(this);
                    ObjectPool.Instance.Return(_prefab);
                }
                break;
            case BuffStatus.Hit:
                break;
            case BuffStatus.Disable:
                ObjectPool.Instance.Return(_prefab);
                break;
            default:
                break;
        }
    }
}
