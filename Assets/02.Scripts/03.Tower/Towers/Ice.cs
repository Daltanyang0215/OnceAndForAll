using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : TowerRangeBase
{
    protected override void Attack()
    {
        foreach (var col in cols)
        {
            if(col.TryGetComponent(out IHitaction enemy))
            {
                enemy.OnHit(damage);
            }
        }
    }
}
