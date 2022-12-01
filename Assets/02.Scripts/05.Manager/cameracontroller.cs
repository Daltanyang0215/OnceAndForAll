using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    private Transform cameratr;
    // Start is called before the first frame update
    void Start()
    {
    
        cameratr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameratr.position += cameratr.rotation* new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"))*Time.deltaTime;
        cameratr.Rotate(new Vector3(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0)) ;
    }
}
