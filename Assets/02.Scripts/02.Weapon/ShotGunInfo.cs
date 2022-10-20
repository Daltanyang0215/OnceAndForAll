using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotBase", menuName = "Weapon/ShotBase")]
public class ShotGunInfo : WeaponInfo
{
    [SerializeField] private float _shotBulletCount;
    public float ShotBulletCount => _shotBulletCount; 
    [SerializeField] private float _shotRange;
    public float ShotRange => _shotRange;

}
