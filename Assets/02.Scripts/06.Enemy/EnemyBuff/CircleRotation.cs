using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed*Time.deltaTime);
    }
}
