using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    Control control;
    public Transform armComponent;

    // Start is called before the first frame update
    void Start()
    {
        control = FindObjectOfType<Control>();
        armComponent.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.up * control.baseAngle;

        float newArmAngle = map(control.armAngle, 0, 180, 90, -90);
        armComponent.transform.eulerAngles = new Vector3(newArmAngle, transform.eulerAngles.y, 0);
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
