using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPrefab : MonoBehaviour, IHitaction
{
    [SerializeField] private float _sheildHPInit;
    private float _shieldHP;

    private void OnEnable()
    {
        _shieldHP = _sheildHPInit;
    }

    public void OnHit(float hitDamage)
    {
        _shieldHP -= hitDamage;
        if (_shieldHP <= 0)
            ObjectPool.Instance.Return(gameObject);
    }
}
