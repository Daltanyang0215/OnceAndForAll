using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyBuffs
{
    None,
    Shild,
    EnegyShild,
    BulkUp,
    Smoke,
    Immortality,
    Phantom,
    ForceField,
    Barrier
}
public class EnemyBuffManager : MonoBehaviour
{
    private static EnemyBuffManager instance;
    public static EnemyBuffManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyBuffManager();
            }
            return instance;
        }
    }
    public EnemyBuffBase GetEnemyBuff(EnemyBuffs buff, Enemy owner)
    {
        switch (buff)
        {
            case EnemyBuffs.None:
                break;
            case EnemyBuffs.Shild:
                break;
            case EnemyBuffs.EnegyShild:
                break;
            case EnemyBuffs.BulkUp:
                return new BulkUp(owner);
            case EnemyBuffs.Smoke:
                break;
            case EnemyBuffs.Immortality:
                break;
            case EnemyBuffs.Phantom:
                break;
            case EnemyBuffs.ForceField:
                break;
            case EnemyBuffs.Barrier:
                break;
            default:
                break;
        }
        return null;
    }
}
