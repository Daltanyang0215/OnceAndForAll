using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField] private GameObject _ghostTower;
    [SerializeField] private TowerInfo _tower;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private Transform _towerParnets;

    // ���� ����
    [SerializeField] private Color _possibleColor;
    [SerializeField] private Color _impossibleColor;

    // �����뼱
    [SerializeField] private Vector2 _leftdownPoint;
    [SerializeField] private Vector2 _rightppPoint;
    private Camera _camera;
    private RaycastHit _hit;
    private Vector3 tmpVec;

    private void Start()
    {
        _camera = Camera.main;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 2000f, _groundLayer))
        {
            tmpVec = Vector3.zero;

            // 2.5f ������ �׸��� ����
            tmpVec = _hit.point;
            tmpVec.x = Mathf.RoundToInt(tmpVec.x * 0.4f) * 2.5f;
            tmpVec.y = 0;
            tmpVec.z = Mathf.RoundToInt(tmpVec.z * 0.4f) * 2.5f;

            // ���̰� Ÿ�� ���� Ȯ�� Ÿ���̸� �Ұ������� �÷� ����
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, 2000f, _blockLayer))
            {
                    _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _impossibleColor;
            }
            else
            {
                // Ÿ���� ��ġ������ ��ġ�� ������� Ȯ��
                if (tmpVec.x > _leftdownPoint.x && tmpVec.x < _rightppPoint.x &&
                    tmpVec.z > _leftdownPoint.y && tmpVec.z < _rightppPoint.y)
                {
                    if (MainGameManager.Instance.Money >= _tower.BuyCost)
                    {
                        // ������ �������� ����
                        _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _possibleColor;

                        // ��Ŭ���� Ÿ�� ��ġ
                        if (Input.GetMouseButtonUp(0))
                        {
                            Instantiate(_tower.TowerPrefab, tmpVec, Quaternion.identity, _towerParnets);

                            MainGameManager.Instance.Money -= _tower.BuyCost;

                            // �½���Ʈ�� ������ ���ȿ��� Ÿ�������� �Ȳ����� ����
                            if (!Input.GetKey(KeyCode.LeftShift))
                                MainUIManager.instance.isShowBuilder = false;
                        }
                    }
                    else
                    {
                        _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _impossibleColor;
                    }
                }
                else
                {
                    _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _impossibleColor;
                }
            }

            // ��Ŭ���� Ÿ�� ��ġ ���
            if (Input.GetMouseButtonDown(1))
            {
                MainUIManager.instance.isShowBuilder = false;
            }

            // ��������Ʈ�� ����Ʈ ��ȯ
            _ghostTower.transform.position = tmpVec;
        }
    }

    public void SetBuilderTower(TowerInfo towerInfo)
    {
        _tower = towerInfo;
    }
}
