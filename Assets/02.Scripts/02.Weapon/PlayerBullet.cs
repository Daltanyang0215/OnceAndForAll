using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private RaycastHit _hit;
    private float _damage;
    private float _bulletSpeed;
    private LayerMask _targetlayer;
    private float timer = 5;

    public void Setup(float damage, float bulletSpeed, LayerMask targetlayer)
    {
        _damage = damage;
        _bulletSpeed = bulletSpeed;
        _targetlayer = targetlayer;
        timer = 5;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_bulletSpeed * Time.deltaTime));

        if (Physics.Raycast(transform.position, transform.forward, out _hit, _bulletSpeed * Time.deltaTime, _targetlayer))
        {
            ObjectPool.Instance.Spawn("HitEffect", _hit.point);

            if (_hit.collider.TryGetComponent(out IHitaction hitaction))
            {
                hitaction.OnHit(_damage);
            }
            ObjectPool.Instance.Return(gameObject);
        }

        timer -= Time.deltaTime;
        if (timer < 0)
            ObjectPool.Instance.Return(gameObject);
    }
}
