using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    Control control;
    
    void Start()
    {
        control = FindObjectOfType<Control>();
    }

    private void OnMouseDown()
    {
        if (control.sp.IsOpen == false) control.sp.Open();
        control.angle--;
        control.sp.Write(control.angle.ToString());
        print(control.angle);
    }
}
