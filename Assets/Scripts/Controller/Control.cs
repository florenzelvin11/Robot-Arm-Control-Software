using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    // Serial Port Instance
    public SerialPort serial = new SerialPort("COM8", 9600);

    // Sending to serial port
    private float nextTime;
    bool prevState = false, status = false;

    // Text UI display
    public Text baseAngleText;
    public Text armAngleText;
    public Text foreArmAngleText;
    public Text wristRollText;
    public Text wristPitchText;
    // Initialisation setup
    private void Start()
    {
        if (!serial.IsOpen)
        {
            serial.Open();
            serial.WriteTimeout = 1;
        }
        baseAngle = 0;
        armAngle = 144;
        foreArmAngle = 180;
        wristRollAngle = 90;
        wristPitchAngle = 0;

        nextTime = Time.time;
    }

    // Looping Sequences
    private void Update()
    {
        baseAngleText.text = baseAngle.ToString();
        armAngleText.text = armAngle.ToString();
        foreArmAngleText.text = foreArmAngle.ToString();
        wristRollText.text = wristRollAngle.ToString();
        wristPitchText.text = wristPitchAngle.ToString();

        MoveEveryFrame();
    }

    // Robot Angles
    public float baseAngle;
    public float armAngle;
    public float foreArmAngle;
    public float wristRollAngle;
    public float wristPitchAngle;

    public void updateBaseAngle(float base_val)
    {
        baseAngle = base_val;
        //WriteToSerial();    // substring(0,3)  000############  
        
    }

    public void updateArmAngle(float arm_val)
    {
        this.armAngle = arm_val;
        //WriteToSerial();    // substring(3,6)   ###000#########
    }

    public void updateForeArmAngle(float foreArm_val)
    {
        this.foreArmAngle = foreArm_val;
        //WriteToSerial();    // substring(6,9)   ######000######
    }

    public void updateWristRoll(float wristRoll_val)
    {
        this.wristRollAngle = wristRoll_val;
        //WriteToSerial();    // substring(9,12)  #########000###
    }

    public void updateWristPitch(float wristPitch_val)
    {
        this.wristPitchAngle = wristPitch_val;
        //WriteToSerial();    // substring(12,15) ############000 
    }

    private void WriteToSerial()
    {
        serial.Write("<" + baseAngle.ToString() + "," + armAngle.ToString() + "," + foreArmAngle.ToString() + "," + wristRollAngle.ToString() + "," + wristPitchAngle.ToString() + ">" );
    }

    public void PlayPosition()
    {
        WriteToSerial();
    }

    public void FreeMovement()
    {
        if (prevState == false)
        {
            status = true;
            prevState = true;
        }
        else
        {
            status = false;
            prevState = false;
        }
    }
    
    private void MoveEveryFrame()
    {
        if (status == true)
        {
            if (Time.time > nextTime)
            {
                if (!serial.IsOpen)
                {
                    serial.Open();

                }

                if (serial.IsOpen)
                {
                    WriteToSerial();
                    nextTime = Time.time + 1;
                }
            }
        }
    }

}
