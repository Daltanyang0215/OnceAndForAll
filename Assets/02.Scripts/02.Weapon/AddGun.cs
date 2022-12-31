using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Projectile,
    Range,
    Lift,
    Destroy
}

public class AddGun : Weapon
{
    private Element _currentEle;
    private TowerType _seletTower;
    private RaycastHit _rayhit;
    private bool _isUpgrade;

    public override float Attack()
    {
        return 0;
    }

    protected override void WaeponAction(bool isAction)
    {
        if (isAction == false) return;

        if (_isUpgrade) // ���׷��̵� ���ϋ��� ������ ��ȯ
        {

            if (_currentEle == Element.Electricity)
                _currentEle = 0;
            else
                _currentEle++;
            MainUIManager.instance.SelectedOrb(_currentEle);
        }
        else // Ÿ�� ���� ���϶��� Ÿ���� ��ȯ
        {
            if (_seletTower == TowerType.Destroy)
                _seletTower = 0;
            else
                _seletTower++;
            MainUIManager.instance.SelectedTower(_seletTower);
        }

    }

    private void Update()
    {
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _rayhit, 500f, targetLayer))
        {
            if (_isUpgrade)
            {
                if (_rayhit.collider.TryGetComponent(out TowerBase tower))
                {
                    MainUIManager.instance.SetTowerInfoPanel(tower);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (StatesEnforce.Instance.getElementCount(_currentEle) > 0)
                        {
                            if (tower.OnUpgrad(_currentEle))
                            {
                                StatesEnforce.Instance.AddElement(_currentEle, -1);
                                MainUIManager.instance.SelectedOrb(_currentEle);

                                MainUIManager.instance.SetTowerInfoPanel();
                                MainUIManager.instance.SetTowerInfoPanel(tower);
                            }
                        }
                    }
                }
            }
            else
            {
                MainUIManager.instance.SelectedTower(_seletTower);
                // Ÿ�� �Ǽ� ���� ��� ���� �ʿ�
            }
        }
        else
        {
            MainUIManager.instance.SetTowerInfoPanel();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _isUpgrade = !_isUpgrade;
            MainUIManager.instance.ShowUpgrade(true, _isUpgrade);
            MainUIManager.instance.SetTowerInfoPanel();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        MainUIManager.instance.ShowUpgrade(true,_isUpgrade);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        MainUIManager.instance.ShowUpgrade(false, _isUpgrade);
    }
}
