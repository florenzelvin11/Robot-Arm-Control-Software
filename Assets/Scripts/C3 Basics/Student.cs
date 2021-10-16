using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    // Data
    private string _name;
    private string _major;
    private int _age;
    private float _gpa;

    // Static variables
    public static int studentCount = 0;

    // Constructors
    public Student(string name_init, string major_init, int age_init) {
        _name = name_init;
        _major = major_init;
        _age = age_init;

        studentCount++; // Increments the amount of student attended
    }

    // Getters and setters
    public string Name { get { return _name; } set { _name = value; } }
    public string Major { get { return _major; } set { _major = value; } }
    public int Age { get { return _age; } set { if (value > 0) _age = value; } }
    public float GPA { get{ return _gpa;  } set { if (value >= 0.0 && value <= 7.0) _gpa = value; } }

}
