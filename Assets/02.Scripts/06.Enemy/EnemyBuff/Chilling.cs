using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chilling : EnemyBuffBase
{
    private float chillingtime;
    private float timer;
    private float slowgain;
    private GameObject _prefab;
    private float originSpeed;
    public Chilling(Enemy owner, float chillingtime, float slowgain) : base(owner)
    {
        this.chillingtime = chillingtime;
        this.slowgain = slowgain;
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                if (owner.IsDead) return;
                _prefab = ObjectPool.Instance.Spawn("Chilling", owner.transform.position, owner.transform);

                ParticleSystem[] tmp = _prefab.GetComponentsInChildren<ParticleSystem>();
                ParticleSystem.ShapeModule shape;
                
                foreach (ParticleSystem particle in tmp)
                {
                    shape = particle.shape;
                    shape.skinnedMeshRenderer = owner.GetComponentInChildren<SkinnedMeshRenderer>();
                }
                
                originSpeed = owner.MoveSpeed;
                owner.MoveSpeed -= owner.MoveSpeed * 0.01f * slowgain;
                timer = chillingtime;
                
                break;
            case BuffStatus.Update:
                timer -= Time.deltaTime;
                if (timer <= 0 || owner.IsDead)
                {
                    owner.MoveSpeed = originSpeed;
                    owner.RemoveBuff(this);
                    ObjectPool.Instance.Return(_prefab);
                }
                break;
            case BuffStatus.Hit:
                break;
            case BuffStatus.Disable:
                break;
            default:
                break;
        }
    }
}
