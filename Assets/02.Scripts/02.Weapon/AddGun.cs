using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGun : Weapon
{
    private Element _currentEle;
    private RaycastHit _rayhit;

    public override float Attack()
    {
        return 0;
    }

    protected override void WaeponAction(bool isAction)
    {
        if (isAction == false) return;

        if (_currentEle == Element.Electricity)
            _currentEle = 0;
        else
            _currentEle++;

        MainUIManager.instance.ShowAddBulletCount(_currentEle);
    }

    private void Update()
    {
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _rayhit, 500f, targetLayer))
        {
            if (_rayhit.collider.TryGetComponent(out TowerBase tower))
            {
                MainUIManager.instance.ShowTowerInfoPanel(tower);
                if (Input.GetMouseButtonDown(0))
                {
                    if (StatesEnforce.Instance.getElementCount(_currentEle) > 0)
                    {
                        if (tower.OnUpgrad(_currentEle))
                        {
                            StatesEnforce.Instance.AddElement(_currentEle, -1);
                            MainUIManager.instance.ShowAddBulletCount(_currentEle);
                        }
                    }
                }
            }
        }
        else
        {
            MainUIManager.instance.ShowTowerInfoPanel();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        MainUIManager.instance.ShowWeaponCircle(WeaponType.Add, true);
        MainUIManager.instance.ShowAddBulletCount(_currentEle);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        MainUIManager.instance.ShowWeaponCircle(WeaponType.Add, false);
    }
}
