using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddGun : Weapon
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _blockLayer;
    private Element _currentEle;
    private TowerType _selectTower;
    private RaycastHit _rayhit;
    private bool _isUpgrade;

    private Vector3 tmpVec;

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
            if (_selectTower == TowerType.Destroy)
                _selectTower = 0;
            else
                _selectTower++;

            MainUIManager.instance.SelectedTower(_selectTower);
        }

    }

    private void Update()
    {
        if (_isUpgrade)
        {
            if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _rayhit, 500f, targetLayer))
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

                                animator.SetTrigger("Shot");
                            }
                        }
                    }
                }
            }
            else
            {
                MainUIManager.instance.SetTowerInfoPanel();
            }
        }
        else
        {
            // Ÿ�� �Ǽ� ���� ��� ���� �ʿ�
            if (_selectTower == TowerType.Destroy)
            {
                if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _rayhit, 500f, targetLayer))
                {
                    if (_rayhit.collider.TryGetComponent(out TowerBase tower))
                    {
                        TowerManager.instance.towerBuildPoint.transform.position = _rayhit.collider.transform.position + Vector3.up * 0.01f;
                        TowerManager.instance.towerBuildPoint.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.25f, 0.25f);
                        MainUIManager.instance.SetTowerInfoPanel(tower);
                        if (Input.GetMouseButtonDown(0))
                        {
                            Destroy(tower.gameObject);
                            MainUIManager.instance.SetTowerInfoPanel();
                            animator.SetTrigger("Shot");
                        }
                    }
                }
                else
                {
                    MainUIManager.instance.SetTowerInfoPanel();
                    TowerManager.instance.towerBuildPoint.transform.position = Vector3.forward * -100;
                }
            }
            else
            {
                MainUIManager.instance.SetTowerInfoPanel(TowerManager.instance.towerlist[(int)_selectTower].TowerPrefab.GetComponent<TowerBase>());
                if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _rayhit, 500f, _groundLayer))
                {
                    tmpVec = Vector3.zero;

                    // 2.5f ������ �׸��� ����
                    tmpVec = _rayhit.point;
                    tmpVec.x = Mathf.RoundToInt(tmpVec.x * 0.4f) * 2.5f;
                    tmpVec.y = 0;
                    tmpVec.z = Mathf.RoundToInt(tmpVec.z * 0.4f) * 2.5f;

                    TowerManager.instance.towerBuildPoint.transform.position = tmpVec + Vector3.up * 0.01f;
                    // ��ġ ��ġ�� ǥ�� ���� ����
                    if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, 500f, _blockLayer))
                    {
                        TowerManager.instance.towerBuildPoint.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.25f, 0.25f);
                    }
                    else
                    {
                        TowerManager.instance.towerBuildPoint.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 1f, 0.5f);
                        // Ÿ���� ��ġ������ ��ġ�� ������� Ȯ��
                        if (tmpVec.x > -60.1f && tmpVec.x < 60.1f &&
                        tmpVec.z > -2.6f && tmpVec.z < 197.6f)
                        {
                            // �븻 ���갡 ����������, ��Ŭ���� Ÿ�� ��ġ
                            if (Input.GetMouseButtonUp(0)
                            && StatesEnforce.Instance.getElementCount(Element.Normal) > 0)
                            {
                                Instantiate(TowerManager.instance.towerlist[(int)_selectTower].TowerPrefab, tmpVec, Quaternion.identity, TowerManager.instance.gameObject.transform);

                                // �ڽ�Ʈ ���� ��ȹ�� �����Ǹ� �߰� ����
                                StatesEnforce.Instance.AddElement(Element.Normal, -1); // �ڽ�Ʈ ����
                                MainUIManager.instance.SelectedTower(_selectTower); // ui������Ʈ
                                animator.SetTrigger("Shot");
                            }
                        }
                        else
                        {
                            TowerManager.instance.towerBuildPoint.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.25f, 0.25f);
                        }
                    }


                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TowerManager.instance.towerBuildPoint.SetActive(_isUpgrade);

            _isUpgrade = !_isUpgrade;
            MainUIManager.instance.ShowUpgrade(true, _isUpgrade);
            
            MainUIManager.instance.SelectedTower(_selectTower); // ui������Ʈ
            MainUIManager.instance.SelectedOrb(_currentEle);

            MainUIManager.instance.SetTowerInfoPanel();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TowerManager.instance.towerBuildPoint.SetActive(!_isUpgrade);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        TowerManager.instance.towerBuildPoint.SetActive(false);
    }
}
