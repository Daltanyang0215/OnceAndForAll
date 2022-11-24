using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrier : MonoBehaviour , IHitaction
{
    private int _barrierCount =10;

    private void OnEnable()
    {
        _barrierCount = 10;
    }

    public void OnHit(float hitDamage)
    {
        _barrierCount--;
        if (_barrierCount <= 0)
            ObjectPool.Instance.Return(gameObject);
    }
}
