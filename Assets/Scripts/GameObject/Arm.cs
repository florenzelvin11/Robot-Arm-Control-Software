using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
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
        float newAngle = map(control.armAngle, 0, 180, 90, -90);
        transform.eulerAngles = new Vector3( newAngle, transform.parent.eulerAngles.y, 0);
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
