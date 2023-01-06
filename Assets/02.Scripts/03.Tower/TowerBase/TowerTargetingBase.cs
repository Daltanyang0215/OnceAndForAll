using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    First,
    Last,
    Strong,
    weak
}
public abstract class TowerTargetingBase : TowerBase
{
    [Space]
    [Header("RotateAnchor")]
    [SerializeField] private Transform _rotateX;
    [SerializeField] private Transform _rotateY;
    [SerializeField] protected Transform firePoint;

    [Space]
    [Header("TargetType")]
    [SerializeField] private TargetType _targetType;

    protected Transform target;
    protected Collider[] cols;

    public abstract override void OnApply();

    public void ChangeTargetType(TargetType newtype) => _targetType = newtype;

    protected override void Update()
    {
        base.Update();

        // 타겟이 없다면 새로운 타겟을 검색
        if (target == null)
        {
            // 주위 몬스터 스캔
            if (Physics.CheckSphere(transform.position, attackRange, targetLayer))
            {
                // 주위 몬스터가 감지되면
                cols = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
                // 디폴트 0 번 넣기
                target = cols[0].transform;
                // 타겟팅 타입에 따라 타겟 변경
                switch (_targetType)
                {
                    case TargetType.First:
                        // z 값이 제일 작은 몬스터 검색
                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (cols[i].transform.position.z < target.position.z)
                                target = cols[i].transform;
                        }
                        break;
                    case TargetType.Last:
                        // z 값이 제일 큰 몬스터 검색
                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (cols[i].transform.position.z > target.position.z)
                                target = cols[i].transform;
                        }
                        break;
                    case TargetType.Strong:
                        break;
                    case TargetType.weak:
                        break;
                    default:
                        break;
                }
            }
        }
        // 타겟이 있다면 공격 관련 동작
        else
        {
            // 타겟이 비활성화 상태이나, 공격거리를 벗어나면 타겟에서 해제
            if (target.gameObject.GetComponent<Enemy>().IsDead||
                Vector3.Distance(target.position, transform.position) > attackRange)
            {
                target = null;
            }
            else
            {
                // y 축 회전
                Vector3 tmp = target.position;
                tmp.y = _rotateY.position.y;
                _rotateY.LookAt(tmp);

                // x 축 회전
                _rotateX.LookAt(target);
                // 재장전이 완료되어 공격을 시도

                if (_isLoading == false)
                {
                    _isLoading = true;
                    Attack();
                }
            }
        }
    }

    abstract protected void Attack();
}
