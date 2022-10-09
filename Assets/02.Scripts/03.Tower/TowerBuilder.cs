using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField] private GameObject _ghostTower;
    [SerializeField] private GameObject _Tower;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Color _possibleColor;
    [SerializeField] private Color _impossibleColor;

    // �����뼱
    [SerializeField] private Vector2 _leftdownPoint;
    [SerializeField] private Vector2 _rightppPoint;
    private Camera _camera;
    private RaycastHit _hit;

    private void Start()
    {
        _camera = Camera.main;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 200f, _groundLayer))
        {
            Vector3 tmpVec = Vector3.zero;


            // ���̰� Ÿ�� ���� Ȯ�� Ÿ���̸� �Ұ������� �÷� ����
            if (_hit.collider.tag == "Tower")
            {
                tmpVec = _hit.collider.transform.position;
                _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _impossibleColor;
            }
            else
            {
                // 2.5f ������ �׸��� ����
                tmpVec = _hit.point;
                tmpVec.x = Mathf.RoundToInt(tmpVec.x * 0.4f) * 2.5f;
                tmpVec.y = 0;
                tmpVec.z = Mathf.RoundToInt(tmpVec.z * 0.4f) * 2.5f;

                // Ÿ���� ��ġ������ ��ġ�� ������� Ȯ��
                if (tmpVec.x > _leftdownPoint.x && tmpVec.x < _rightppPoint.x &&
                    tmpVec.z > _leftdownPoint.y && tmpVec.z < _rightppPoint.y)
                {
                    // ������ �������� ����
                    _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _possibleColor;

                    // ��Ŭ���� Ÿ�� ��ġ
                    if (Input.GetMouseButtonUp(0))
                    {
                        Instantiate(_Tower, tmpVec, Quaternion.identity);

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

            // ��Ŭ���� Ÿ�� ��ġ ���
            if (Input.GetMouseButtonDown(1))
            {
                MainUIManager.instance.isShowBuilder = false;
            }

            // ��������Ʈ�� ����Ʈ ��ȯ
            _ghostTower.transform.position = tmpVec;
        }
    }
}
