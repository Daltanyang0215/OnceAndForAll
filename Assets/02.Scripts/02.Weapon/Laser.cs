using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    [SerializeField] private ParticleSystem _chargeParticle;

    private bool _isCharge;

    protected override void WaeponAction(bool isAction )
    {
        if (_isCharge)
        {
            animator.SetBool("DoCharge", false);
            fireParticale.Stop();
            _isCharge = false;
        }
        else
        {
            animator.SetBool("DoCharge", true);
            fireParticale.Play();
            _isCharge = true;
        }
    }

    public void ShotLaser()
    {
        Shot();
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
