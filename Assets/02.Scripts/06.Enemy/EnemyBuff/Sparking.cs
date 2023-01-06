using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparking : EnemyBuffBase
{
    private float sparkingTime;
    private float timer;
    private float damageGain;
    private GameObject _prefab;

    public Sparking(Enemy owner,float sparkingTime, float damageGain) : base(owner)
    {
        this.sparkingTime = sparkingTime;
        this.damageGain = damageGain; 
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                _prefab = ObjectPool.Instance.Spawn("Sparking", owner.transform.position,owner.transform);
                ParticleSystem.ShapeModule shape = _prefab.GetComponent<ParticleSystem>().shape;
                shape.skinnedMeshRenderer = owner.GetComponentInChildren<SkinnedMeshRenderer>();
                timer = sparkingTime;
                break;
            case BuffStatus.Update:
                timer -= Time.deltaTime;
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

    public override float HitActive(float damage)
    {
        return damage*damageGain;
    }
}
