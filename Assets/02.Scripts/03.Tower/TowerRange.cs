using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : Tower
{
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private ParticleSystem _rangeEffect;
    [SerializeField] private SphereCollider _rangeCollider;

    private List<Enemy> _targetList= new List<Enemy>();
    
    protected override void Update()
    {
        base.Update();
        Attack();
    }

    public override void Onapply()
    {
        base.Onapply();
        _rangeCollider.radius = attackRange;
        _rangeEffect.transform.localScale = Vector3.one * StatesEnforce.Instance.TowerRangeGain;
    }

    private void Attack()
    {
        for (int i = 0; i < _targetList.Count; i++)
        {
            if(_targetList[i].Hit(_damage * Time.deltaTime))
                _targetList.RemoveAt(i);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null &&
            other.TryGetComponent(out Enemy enemy))
        {
            enemy.Slow(true);
            _targetList.Add(enemy);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null &&
            other.TryGetComponent(out Enemy enemy))
        {
            enemy.Slow(false);
            _targetList.Remove(enemy);
        }
    }

}
