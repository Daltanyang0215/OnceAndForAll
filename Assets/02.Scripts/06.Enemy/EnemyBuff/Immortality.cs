using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortality : EnemyBuffBase
{
    private List<Enemy> _activeList = new List<Enemy>();
    private Collider[] _tmpcols;
    private GameObject tmpPrefab;
    public Immortality(Enemy owner) : base(owner)
    {
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                owner.gameObject.transform.localScale = Vector3.one * 1.5f;
                tmpPrefab= ObjectPool.Instance.Spawn("Immortality", owner.transform.position, Quaternion.identity, owner.transform);
                break;
            case BuffStatus.Update:
                foreach (Enemy enemy in _activeList)
                {
                    enemy.isImmortality = false;
                }
                _activeList.Clear();
                if (owner.IsDead) return;

                _tmpcols = Physics.OverlapSphere(owner.transform.position, 15f,1<<20);
                foreach (Collider col in _tmpcols)
                {
                    if (col.TryGetComponent(out Enemy newEnemy))
                    {
                        if (newEnemy.CheckBuff(typeof(Immortality))) continue;
                        newEnemy.isImmortality = true;
                        _activeList.Add(newEnemy);
                    }
                }
                break;
            case BuffStatus.Hit:
                break;
            case BuffStatus.Disable:
                ObjectPool.Instance.Return(tmpPrefab);
                break;
            default:
                break;
        }
    }
}
