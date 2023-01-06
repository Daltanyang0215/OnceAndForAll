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

        // Ÿ���� ���ٸ� ���ο� Ÿ���� �˻�
        if (target == null)
        {
            // ���� ���� ��ĵ
            if (Physics.CheckSphere(transform.position, attackRange, targetLayer))
            {
                // ���� ���Ͱ� �����Ǹ�
                cols = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
                // ����Ʈ 0 �� �ֱ�
                target = cols[0].transform;
                // Ÿ���� Ÿ�Կ� ���� Ÿ�� ����
                switch (_targetType)
                {
                    case TargetType.First:
                        // z ���� ���� ���� ���� �˻�
                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (cols[i].transform.position.z < target.position.z)
                                target = cols[i].transform;
                        }
                        break;
                    case TargetType.Last:
                        // z ���� ���� ū ���� �˻�
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
        // Ÿ���� �ִٸ� ���� ���� ����
        else
        {
            // Ÿ���� ��Ȱ��ȭ �����̳�, ���ݰŸ��� ����� Ÿ�ٿ��� ����
            if (target.gameObject.GetComponent<Enemy>().IsDead||
                Vector3.Distance(target.position, transform.position) > attackRange)
            {
                target = null;
            }
            else
            {
                // y �� ȸ��
                Vector3 tmp = target.position;
                tmp.y = _rotateY.position.y;
                _rotateY.LookAt(tmp);

                // x �� ȸ��
                _rotateX.LookAt(target);
                // �������� �Ϸ�Ǿ� ������ �õ�

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
