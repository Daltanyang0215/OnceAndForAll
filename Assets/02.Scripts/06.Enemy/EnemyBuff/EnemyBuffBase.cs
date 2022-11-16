using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffBase : MonoBehaviour
{
    private Enemy _owner;

    private void OnEnable()
    {
        _owner = GetComponent<Enemy>();
    }

    private void OnDisable()
    {
        Destroy(this);   
    }

}
