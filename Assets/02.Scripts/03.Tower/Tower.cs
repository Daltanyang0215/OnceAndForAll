using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerInfo _info;
    

    [SerializeField] private Transform rotatePoint;
    [SerializeField] protected float detectRange;
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected LayerMask _blockLayer;
    protected Transform target;

    protected virtual void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRange, _targetLayer);

        if (cols.Length > 0)
        {
            target = cols[0].transform;
            
            rotatePoint.LookAt(target.position + Vector3.up);
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,detectRange);
    }
}
