﻿using System;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("References")] public Transform wheelPivot;
    public HingeJoint wheelJoint;

    private JointMotor _motor;

    private void Awake()
    {
        _motor = wheelJoint.motor;
    }

    void Update()
    {
        wheelPivot.rotation = Quaternion.AngleAxis(wheelJoint.angle, wheelPivot.right) * wheelPivot.rotation;
    }

    public void SetTorque(float torque)
    {
        _motor.targetVelocity = torque;
        wheelJoint.motor = _motor;
    }
}