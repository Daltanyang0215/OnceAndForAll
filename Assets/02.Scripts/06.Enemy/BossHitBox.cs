using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour, IHitaction
{
    private Boss _root;
    [SerializeField] private float _damageGain=1;

    private void Awake()
    {
        _root = transform.GetComponentInParent<Boss>();
    }

    public void OnHit(float hitDamage)
    {
        _root.OnHit(hitDamage* _damageGain);
    }
}
