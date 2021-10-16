using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    Control control;
    public Transform armComponent;
    public Transform foreArmComponent;
    public Transform wristRollComponent;
    public Transform wristPitchComponent;

    // Start is called before the first frame update
    void Start()
    {
        control = FindObjectOfType<Control>();
        armComponent.parent = transform;
        foreArmComponent.parent = armComponent.transform;
        wristRollComponent.parent = foreArmComponent.transform;
        wristPitchComponent.parent = wristRollComponent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float newBaseAngle = map(control.baseAngle, 0, 180, 180, 0);
        float newArmAngle = map(control.armAngle, 0, 180, 90, -90);
        float newForeArmAngle = map(control.foreArmAngle, 0, 180, -90, 90);
        float newRollAngle = map(control.wristRollAngle, 0, 180, 180, 0);
        float newPitchAngle = map(control.wristPitchAngle, 0, 180, 90, -90);

        transform.eulerAngles = Vector3.up * newBaseAngle;     // Base Movement
        armComponent.transform.eulerAngles = new Vector3(newArmAngle, transform.eulerAngles.y, 0);      // Arm Movement
        foreArmComponent.transform.localRotation = Quaternion.Euler(newForeArmAngle, 0, armComponent.transform.eulerAngles.z); // ForeArm Movement
        wristRollComponent.transform.localRotation = Quaternion.Euler(0, newRollAngle, 0);
        wristPitchComponent.transform.localRotation = Quaternion.Euler(0, 0, newPitchAngle);
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
