using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerInfo _info;

    [SerializeField] private Transform _rotatePoint;
    [SerializeField] protected float attackRange;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected LayerMask blockLayer;
    protected Transform target;

    [SerializeField] protected float _damage;

    private void Start()
    {
        StateInit();
    }

    public void StateInit()
    {
        _damage = _info.Damage * StatesEnforce.Instance.TowerDamageGain;
        attackRange = _info.AttackRange * StatesEnforce.Instance.TowerRangeGain;
    }

    protected virtual void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

        
        if (cols.Length > 0)
        {
            target = cols[0].transform;
            
            _rotatePoint.LookAt(target);
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}
