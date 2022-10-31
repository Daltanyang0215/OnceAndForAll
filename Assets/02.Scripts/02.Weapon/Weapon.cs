using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponInfo weaponInfo;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private TMP_Text _bulletCountText;
    [SerializeField] protected ParticleSystem fireParticale;
    protected Animator animator;

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

    protected Camera maincamera;
    protected RaycastHit _hit;

    protected float damage;

    // 공격 쿨타임인지 확인
    protected bool _isAttackCool;
    // 재장전 중인지 확인
    private bool _isReloading;

    // 특수기 관련 토글
    private bool _isWeaponAction;
    public bool isWeaponAction
    {
        get
        {
            return _isWeaponAction;
        }
        set
        {
            if(_isWeaponAction != value)
            {
                _isWeaponAction = value;
                WaeponAction(_isWeaponAction);
            }
        }
    }

    // 공격 (player script call 전용)
    public float Attack()
    {
        if (!_isAttackCool)
        {

            // 디버그. 장전 애니메이션 추가 시 삭제 예정
            if (_isReloading) Debug.Log("재장전 취소");

            //

            ReloadTimeReset();
            _isAttackCool = true;

            if (CurrentBullet > 0)
            {
                CurrentBullet--;
                _attackCoolTimer = weaponInfo.AttackCool;
                Shot();
                animator.SetTrigger("Shot");
                fireParticale.Play();
                return weaponInfo.Rebound;
            }
            else
            {
                _attackCoolTimer = weaponInfo.AttackCool * 0.2f;
                Debug.Log("탄약부족");
            }
        }
        return 0;
    }
    // 재장전 (player script call 전용)
    public virtual void Reload()
    {
        //if (_isAttackCool) return;
        _isReloading = true;
        Debug.Log("재장전 중");
    }

    // 강화 적용
    public virtual void OnApply()
    {
        damage = weaponInfo.Damage * StatesEnforce.Instance.weaponDamageGain;
    }
    // 특수기
    protected virtual void WaeponAction(bool isAction)
    {

    }

    // 초기화 . 상속으로 초기화 대상을 늘릴 수 있게
    protected virtual void Start()
    {
        maincamera = Camera.main;
        CurrentBullet = weaponInfo.MaxBullet;
        _attackCoolTimer = weaponInfo.AttackCool;
        _reLoadTimer = weaponInfo.ReloadTIme;
        animator = GetComponent<Animator>();
    }

    // 실제 공격 로직. 상속으로 오버로드 할 수 있게 설계
    protected virtual void Shot()
    {
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _hit, 200f))
        {
            // 레이포인트에 이펙트 소환
            GameObject go = ObjectPool.Instance.Spawn("HitEffect", _hit.point);
            ObjectPool.Instance.Return(go, 0.3f);

            if (_hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Hit(damage);
            }
        }
    }

    protected virtual void OnEnable()
    {
        OnApply();
        // play animation
    }

    protected virtual void OnDisable()
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
                CurrentBullet = weaponInfo.MaxBullet;
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
        _reLoadTimer = weaponInfo.ReloadTIme;
    }

}
