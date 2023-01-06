using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    [SerializeField] private ParticleSystem _chargeParticle;
    [SerializeField] private LineRenderer _beemLine;

    private bool _isCharge;

    protected override void WaeponAction(bool isAction)
    {
        if (_isCharge)
        {
            animator.SetBool("DoCharge", false);
            _beemLine.gameObject.SetActive(false);
            _chargeParticle.Stop();
            _isCharge = false;
        }
        else
        {
            animator.SetBool("DoCharge", true);
            _beemLine.gameObject.SetActive(true);
            _chargeParticle.Play();
            _isCharge = true;
        }
    }
    // 애니메이션에서 call 할 함수
    public void ShotLaserSubBullet()
    {
        currentBullet--;
        // 탄약이 없다면 레이저 종료
        if (currentBullet <= 0)
        {
            _beemLine.gameObject.SetActive(false);
            _chargeParticle.Stop();
        }
    }
    // 애니메이션에서 call 할 함수
    public void ShotLaser()
    {
        if (currentBullet <= 0) return;

        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _hit, 200f))
        {
            // 레이포인트에 이펙트 소환
            GameObject go = ObjectPool.Instance.Spawn("HitEffect", _hit.point);
            ObjectPool.Instance.Return(go, 0.3f);

            if (_hit.collider.TryGetComponent(out IHitaction enemy))
            {
                enemy.OnHit(damage);
            }
        }
    }

    protected override void Shot()
    {
        fireParticale.Play();
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _hit, 500f, targetLayer))
        {
            // 레이포인트에 이펙트 소환
            GameObject go = ObjectPool.Instance.Spawn("HitEffect", _hit.point);
            ObjectPool.Instance.Return(go, 0.3f);

            if (_hit.collider.TryGetComponent(out IHitaction enemy))
            {
                enemy.OnHit(damage);
            }
        }
        currentBullet -= 9;
    }

    public override void Reload()
    {
        base.Reload();
    }

    public override void OnApply()
    {
        base.OnApply();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }


}
