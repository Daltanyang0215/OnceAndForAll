using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _ghostTower;
    [SerializeField] private GameObject _Tower;
    [SerializeField] private LayerMask _groundLayer;


    // 마지노선
    [SerializeField] private Vector2 _leftdownPoint;
    [SerializeField] private Vector2 _rightppPoint;
    private Camera _camera;
    private RaycastHit _hit;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 200f, _groundLayer))
        {
            // 2.5f 단위로 그리드
            Vector3 tmpVec = _hit.point;
            tmpVec.x = Mathf.RoundToInt(tmpVec.x*0.4f) *2.5f;
            tmpVec.z = Mathf.RoundToInt(tmpVec.z*0.4f) *2.5f;

            if (tmpVec.x > _leftdownPoint.x && tmpVec.x < _rightppPoint.x &&
                tmpVec.z > _leftdownPoint.y && tmpVec.z < _rightppPoint.y)
            {

                // 레이포인트에 이펙트 소환
                _ghostTower.transform.position = tmpVec;

                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(_Tower, tmpVec, Quaternion.identity);
                }
            }
        }
    }
}
