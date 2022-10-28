using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    private bool _isCharge;
    
    public override void WaeponAction()
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
