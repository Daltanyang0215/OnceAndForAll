using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolRetrun : MonoBehaviour
{
    [SerializeField] private float _returnTime=9999f;
    private float _timer;

    private void Update()
    {
        if (_timer <= 0)
            ObjectPool.Instance.Return(gameObject);

        _timer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _timer = _returnTime;
    }
    private void OnDisable()
    {
        ObjectPool.Instance.Return(gameObject);
    }

    private void OnParticleSystemStopped()
    {
        ObjectPool.Instance.Return(gameObject);
    }
}
