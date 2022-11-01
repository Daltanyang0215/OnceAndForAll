using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField] private GameObject _ghostTower;
    [SerializeField] private TowerInfo _tower;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private LayerMask _destroyLayer;
    [SerializeField] private Transform _towerParnets;

    // 상태 색상
    [SerializeField] private Color _possibleColor;
    [SerializeField] private Color _impossibleColor;
    [SerializeField] private Color _destroyColor;

    // 마지노선
    [SerializeField] private Vector2 _leftdownPoint;
    [SerializeField] private Vector2 _rightppPoint;
    private Camera _camera;
    private RaycastHit _hit;
    private Vector3 tmpVec;

    public bool isDestroy;

    private void Start()
    {
        _camera = Camera.main;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isDestroy)
        {
            // 파괴 상태에서 타워를 감지하면
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 2000f, _destroyLayer))
            {
                tmpVec = _hit.collider.transform.position;
                _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _destroyColor;
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(_hit.collider.gameObject);
                    // 좌시프트를 누르는 동안에는 타워빌더가 안꺼지게 설정
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        isDestroy = false;
                        MainUIManager.instance.isShowBuilder = false;
                        tmpVec = Vector3.back * 50f;
                    }
                }
            }
            // 파괴 상태에서 타워를 감지안되면 고스트 타워 치우기
            else
            {
                tmpVec = Vector3.back * 50f;
            }
        }
        else
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 2000f, _groundLayer))
            {
                tmpVec = Vector3.zero;

                // 2.5f 단위로 그리드 동작
                tmpVec = _hit.point;
                tmpVec.x = Mathf.RoundToInt(tmpVec.x * 0.4f) * 2.5f;
                tmpVec.y = 0;
                tmpVec.z = Mathf.RoundToInt(tmpVec.z * 0.4f) * 2.5f;

                // 레이가 타워 인지 확인 타워이면 불가능으로 컬러 변경
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, 2000f, _blockLayer))
                {
                    _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _impossibleColor;
                }
                else
                {
                    // 타워가 설치가능한 위치를 벗어났는지 확인
                    if (tmpVec.x > _leftdownPoint.x && tmpVec.x < _rightppPoint.x &&
                        tmpVec.z > _leftdownPoint.y && tmpVec.z < _rightppPoint.y)
                    {
                        if (MainGameManager.Instance.Money >= _tower.BuyCost)
                        {
                            // 생상을 가능으로 변경
                            _ghostTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _possibleColor;

                            // 좌클릭시 타워 설치
                            if (Input.GetMouseButtonUp(0))
                            {
                                Instantiate(_tower.TowerPrefab, tmpVec, Quaternion.identity, _towerParnets);

                                MainGameManager.Instance.Money -= _tower.BuyCost;

                                isDestroy = false;
                                // 좌시프트를 누르는 동안에는 타워빌더가 안꺼지게 설정
                                if (!Input.GetKey(KeyCode.LeftShift))
                                {
                                    MainUIManager.instance.isShowBuilder = false;
                                    tmpVec = Vector3.back * 50f;
                                }
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

            }
            // 레이가 물체를 감지하지 못하면 고스트 치우기
            else
            {
                tmpVec = Vector3.back * 50f;
            }
        }

        // 우클릭시 타워 설치 취소
        if (Input.GetMouseButtonDown(1))
        {
            MainUIManager.instance.isShowBuilder = false;
            isDestroy = false;
            tmpVec = Vector3.back * 50f;
        }

        // 레이포인트에 고스트 타워 이동
        _ghostTower.transform.position = tmpVec;
    }

    public void SetBuilderTower(TowerInfo towerInfo)
    {
        _tower = towerInfo;
    }

}
