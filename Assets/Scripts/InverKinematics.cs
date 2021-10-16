using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InverKinematics : MonoBehaviour
{
    // Chain length of bones
    public int chainLength = 3;

    public Transform Target;
    public Transform Pole;

    [Header("Solver Parameters")]
    public int iteration = 10;

    public float delta = 0.001f;

    [Range(0, 1)]
    public float SnapBackStrength = 1f;

    // Position
    protected float[] BonesLength;
    protected float CompleteLength;
    protected Transform[] Bones;
    protected Vector3[] Positions;

    // Rotation
    protected Vector3[] StartDirectionSucc;
    protected Quaternion[] StartRotationBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StarRotationRoot;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        // Inital Array
        Bones = new Transform[chainLength + 1];
        Positions = new Vector3[chainLength + 1];
        BonesLength = new float[chainLength];
        
        StartDirectionSucc = new Vector3[chainLength + 1];
        StartRotationBone = new Quaternion[chainLength + 1];

        // Init Field
        if(Target == null)
        {
            Target = new GameObject(gameObject.name + "Target").transform;
            Target.position = transform.position;
        }
        StartRotationTarget = Target.rotation;
        CompleteLength = 0;

        // Init Data
        var current = transform;
        for(var i = Bones.Length - 1; i >= 0; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;

            if(i == Bones.Length - 1)
            {
                // Leaf - arm
                StartDirectionSucc[i] = Target.position - current.position;     // calculates a vector position from the origin to the target
            }
            else
            {
                // Mid bones
                StartDirectionSucc[i] = Bones[i + 1].position - current.position;
                BonesLength[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLength += BonesLength[i];
            }
            current = current.parent;


        }
    }

    private void LateUpdate()
    {
        ResolveIK();
    }


    private void ResolveIK()
    {
        if(Target == null)
        {
            return;
        }

        if(BonesLength.Length != chainLength)
        {
            Init();
        }

        // Get Positions
        for(int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }

 /*       var RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var RootRotDiff = RootRot * Quaternion.Inverse(StarRotationRoot);*/

        if((Target.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
        {
            // Just Strecth it
            var direction = (Target.position - Positions[0]).normalized;  // Gets the direction of the target position in vector3

            // Set everything after the root
            for(int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = Positions[i - 1] + direction * BonesLength[i - 1]; // Updates each arm joints' length corresponding to its location
            }

        }
        else
        {
            for(int i = 0; i < iteration; i++)
            {
                // Back Algorithm
                for(int j = Positions.Length - 1; j > 0; j--)
                {
                    if(j == Positions.Length - 1)
                    {
                        Positions[j] = Target.position;     // Set Head to the target
                    }
                    else
                    {
                        Positions[j] = Positions[j + 1] + (Positions[j] - Positions[j + 1]).normalized * BonesLength[j];  
                    }
                }

                // Forward
                for(int j = 1; j < Positions.Length; j++)
                {
                    Positions[j] = Positions[j - 1] + (Positions[j] - Positions[j - 1]).normalized * BonesLength[j - 1];
                }

                // If the position is close enough, we stop the algorithm
                if ((Positions[Positions.Length - 1] - Target.position).sqrMagnitude < delta * delta) { break; }

            }
        }

        // Set Positions
        for (int i = 0; i < Positions.Length; i++)
        {
            if(i == Positions.Length - 1)
            {
                Bones[i].rotation = Target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            }
            else
            {
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionSucc[i], Positions[i + 1] - Positions[i]) * StartRotationBone[i];
            }
            Bones[i].position = Positions[i];
        }
    }

    private void OnDrawGizmos()
    {
        var current = this.transform;
        for(int i = 0; i < chainLength && current != null && current.parent != null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
        }
    }
}
