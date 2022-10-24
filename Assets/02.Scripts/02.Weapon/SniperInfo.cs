using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperBase", menuName = "Weapon/SniperBase")]
public class SniperInfo : WeaponInfo
{
    [SerializeField] private float _zoomGain;
    public float ZoomGain => _zoomGain;
    
}
