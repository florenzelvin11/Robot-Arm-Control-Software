using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Control control;
    // Start is called before the first frame update
    void Start()
    {
        control = FindObjectOfType<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.up * control.baseAngle;
    }
    
}
