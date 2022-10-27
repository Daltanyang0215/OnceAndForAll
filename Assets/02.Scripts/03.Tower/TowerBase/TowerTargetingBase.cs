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
    [SerializeField] private Transform _rotateX;
    [SerializeField] private Transform _rotateY;
    [SerializeField] private Transform _FirePoint;

    [SerializeField] private TargetType _targetType;

    protected Transform target;
    protected List<Collider> cols = new List<Collider>();

    public abstract override void OnApply();

    public void ChangeTargetType(TargetType newtype) => _targetType = newtype;

    protected override void Update()
    {
        base.Update();
        if (_isLoading == false)
        {
            // Ÿ���� ���ٸ� ���ο� Ÿ���� �˻�
            if (target == null)
            {
                cols.AddRange(Physics.OverlapSphere(transform.position, attackRange, targetLayer));
                if (cols.Count > 0)
                {
                    switch (_targetType)
                    {
                        case TargetType.First:
                            cols.Sort((x, y) => x.transform.position.z.CompareTo(x.transform.position.z));
                            break;
                        case TargetType.Last:
                            cols.Sort((x, y) => x.transform.position.z.CompareTo(x.transform.position.z));
                            cols.Reverse();
                            break;
                        case TargetType.Strong:
                            break;
                        case TargetType.weak:
                            break;
                        default:
                            break;
                    }

                    target = cols[0].transform;

                    // y �� ȸ��
                    Vector3 tmp = target.position;
                    tmp.y = _rotateY.position.y;
                    _rotateY.LookAt(tmp);

                    // x �� ȸ��
                    _rotateX.LookAt(target);
                }
                else
                {
                    target = null;
                }
            }
            // Ÿ���� �ִٸ� ���� ���� ����
            else
            {
                // Ÿ���� Ȱ��ȭ ���¸� Ȯ�� �� Ȱ��ȭ �Ͻÿ� ����
                if (target.gameObject.activeSelf)
                {
                    _isLoading = true;
                    Attack();
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    abstract protected void Attack();
}
