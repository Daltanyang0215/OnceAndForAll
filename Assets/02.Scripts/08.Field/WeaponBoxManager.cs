using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxManager : MonoBehaviour
{
    [SerializeField] private List<WeaponBox> weaponBoxes = new List<WeaponBox>();

    private void Start()
    {
        
    }

    public void ChildBoxAdd(WeaponBox box)
    {
        weaponBoxes.Add(box);
    }

    public void BoxIntaraction()
    {

    }
}
