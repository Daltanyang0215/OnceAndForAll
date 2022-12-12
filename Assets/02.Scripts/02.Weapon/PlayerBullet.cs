using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody _rb;

    private float _damage;
    private float _bulletSpeed;

    public void Setup(float damage , float bulletSpeed)
    {
        _damage= damage;
        _bulletSpeed= bulletSpeed;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHitaction hit))
        {
            hit.OnHit(_damage);
        }
        ObjectPool.Instance.Return(gameObject);
    }
}
