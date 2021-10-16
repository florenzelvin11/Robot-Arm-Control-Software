using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Student bob = new Student("Bob", "Engineering", 20);
        print("Name: " + bob.Name + " | Major: " + bob.Major + " | Age: " + bob.Age);
        print("GPA: " + bob.GPA);
        print("Attendies: " + Student.studentCount);

        Student ross = new Student("Ross", "Art", 69);
        print("Name: " + ross.Name + " | Major: " + ross.Major + " | Age: " + ross.Age);
        print("Attendies: " + Student.studentCount);
    }
}
