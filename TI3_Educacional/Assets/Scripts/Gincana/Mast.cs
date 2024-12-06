using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mast : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    void Start()
    {
        if (ProfileManager.IsManaging)
        {
            speed *= System.Convert.ToSingle(ProfileManager.GetCurrent(ProfileInfo.Info.floatMastSpeed));
        }

        HingeJoint joint = GetComponent<HingeJoint>();
        JointMotor motor = joint.motor;
        motor.force *= speed;
    }
}
