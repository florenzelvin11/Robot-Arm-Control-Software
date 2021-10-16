using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class led : MonoBehaviour
{
    public SerialPort serial = new SerialPort("COM6", 9600);
    private bool lightState = false;
    
    public void OnMouseDown()
    {
        if (serial.IsOpen == false)
        {
            serial.Open();
        }
        if (lightState == false)
        {
            serial.Write("A");
            lightState = true;
        }
        else
        {
            serial.Write("a");
            lightState = false;
        }
    }
}
