using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _fireParticale;
    [SerializeField] private ParticleSystem _hitParticale;

    private int _currentBullet;
    private float _attackCoolTimer;
    private float _reLoadTimer;

    private Camera _camera;
    private RaycastHit _hit;

    // ���� ��Ÿ������ Ȯ��
    protected bool _isAttackCool;
    // ������ ������ Ȯ��
    private bool _isReloading;

    private void Start()
    {
        _camera = Camera.main;
        _currentBullet = _weaponInfo.MaxBullet;
        _attackCoolTimer = _weaponInfo.AttackCool;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }
    private void OnEnable()
    {
        // play animation
    }
    private void OnDisable()
    {
        ReloadTimeReset();
    }
    private void Update()
    {
        // ���� Ÿ�̸�
        if (_isAttackCool)
        {
            if (_attackCoolTimer < 0)
            {
                _isAttackCool = false;
            }
            else
            {
                _attackCoolTimer -= Time.deltaTime;
            }
        }

        // ������ Ÿ�̸�
        if (_isReloading)
        {
            if (_reLoadTimer < 0)
            {
                ReloadTimeReset();
                _currentBullet = _weaponInfo.MaxBullet;
                Debug.Log("������ �Ϸ�");
            }
            else
            {
                _reLoadTimer -= Time.deltaTime;
            }
        }

    }

    // ���� (player script call ����)
    public virtual void Attack()
    {
        if (_isAttackCool) return;

        // �����
        if(_isReloading) Debug.Log("������ ���");
        
        //

        _isAttackCool = true;
        _attackCoolTimer = _weaponInfo.AttackCool;
        ReloadTimeReset();

        if(_currentBullet > 0)
        {
            Debug.Log("��");
            
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward, out _hit,200f))
            {
                // ��������Ʈ�� ����Ʈ ��ȯ
                Destroy( Instantiate(_hitParticale,_hit.point,Quaternion.identity).gameObject,0.3f);
            }
            _currentBullet--;
            _fireParticale.Play();
        }
        else
        {
            Debug.Log("ź�����");
        }
    }

    // ������ (player script call ����)
    public void Reload()
    {
        _isReloading=true;
        Debug.Log("������ ��");
    }

    // ������ ����
    private void ReloadTimeReset()
    {

        _isReloading = false;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }
    
}
