using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private TMP_Text _bulletCountText;
    [SerializeField] private ParticleSystem _fireParticale;
    [SerializeField] private ParticleSystem _hitParticale;

    private int _currentBullet;
    private int CurrentBullet
    {
        get
        {
            return _currentBullet;
        }
        set
        {
            _currentBullet = value;
            _bulletCountText.text = _currentBullet.ToString();
        }
    }
    private float _attackCoolTimer;
    private float _reLoadTimer;

    private Camera _camera;
    private RaycastHit _hit;

    private float _damage;

    // 공격 쿨타임인지 확인
    protected bool _isAttackCool;
    // 재장전 중인지 확인
    private bool _isReloading;

    // 공격 (player script call 전용)
    public virtual void Attack()
    {
        if (_isAttackCool) return;

        // 디버그. 장전 애니메이션 추가 시 삭제 예정
        if (_isReloading) Debug.Log("재장전 취소");

        //

        _isAttackCool = true;
        _attackCoolTimer = _weaponInfo.AttackCool;
        ReloadTimeReset();

        if (CurrentBullet > 0)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 200f))
            {
                // 레이포인트에 이펙트 소환
                GameObject go = ObjectPool.Instance.Spawn("HitEffect", _hit.point);
                ObjectPool.Instance.Return(go, 0.3f);

                if (_hit.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.Hit(_damage);
                }
            }
            CurrentBullet--;
            _fireParticale.Play();
        }
        else
        {
            Debug.Log("탄약부족");
        }
    }

    // 재장전 (player script call 전용)
    public void Reload()
    {
        _isReloading = true;
        Debug.Log("재장전 중");
    }

    // 강화 적용
    public void EnforceApply()
    {
        _damage = _weaponInfo.Damage * StatesEnforce.Instance.weaponDamageGain;
    }

    // 초기화
    private void Start()
    {
        _camera = Camera.main;
        CurrentBullet = _weaponInfo.MaxBullet;
        _attackCoolTimer = _weaponInfo.AttackCool;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }

    private void OnEnable()
    {
        EnforceApply();
        // play animation
    }

    private void OnDisable()
    {
        ReloadTimeReset();
    }

    private void Update()
    {
        // 공격 타이머
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

        // 재장전 타이머
        if (_isReloading)
        {
            if (_reLoadTimer < 0)
            {
                ReloadTimeReset();
                CurrentBullet = _weaponInfo.MaxBullet;
                Debug.Log("재장전 완료");
            }
            else
            {
                _reLoadTimer -= Time.deltaTime;
            }
        }

    }

    // 재장전 실패
    private void ReloadTimeReset()
    {

        _isReloading = false;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }

}
