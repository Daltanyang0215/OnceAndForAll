using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : Tower
{
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private int _projectileSpeed;

    [SerializeField] private float _reloadTime;
    private float _reloadTimer;

    protected override void Update()
    {
        base.Update();
        Reload();
    }

    private void Reload()
    {
        if (_reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                _reloadTimer = _reloadTime;
            }

        }
        else
        {
            _reloadTimer -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        for (int i = 0; i < _firePoints.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.Spawn("BalistaBult", _firePoints[i].position);
            bullet.GetComponent<ProjectileBullet>().SetUp(target,
                                                          _projectileSpeed,
                                                          _damage,
                                                          false,
                                                          blockLayer,
                                                          targetLayer);
        }
    }
}
