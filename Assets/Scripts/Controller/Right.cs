using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    Control control;

    
    void Start()
    {
        control = FindObjectOfType<Control>();
    }

    private void OnMouseDown()
    {
        if (control.sp.IsOpen == false) control.sp.Open();
        control.sp.WriteLine(control.angle.ToString());
        print(control.angle);
    }
}
