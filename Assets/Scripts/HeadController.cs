using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [Range(5f, 175f)] public float swivel = 90f;
    [Range(70, 150f)] public float tilt = 90f;
    public float servoSpeed = 10f;
    public float ultrasonicRadius = 0.005f;

    [Header("References")] public Transform distanceSensor;
    public Camera carCamera;
    public HingeJoint swivelPivot;
    public HingeJoint tiltPivot;

    private void Update()
    {
        var swivelEuler = swivelPivot.angle;
        var targetSwivel = swivel - 90f;
        var swivelMotor = swivelPivot.motor;
        swivelMotor.targetVelocity = Mathf.Approximately(swivelEuler, targetSwivel) ? 0f : targetSwivel - swivelEuler;
        swivelPivot.motor = swivelMotor;

        var tiltEuler = tiltPivot.angle;
        var targetTilt = 90f - tilt;
        var tiltMotor = tiltPivot.motor;
        tiltMotor.targetVelocity = Mathf.Approximately(tiltEuler, targetTilt) ? 0f : targetTilt - tiltEuler;
        tiltPivot.motor = tiltMotor;
    }

    public float GetDistance()
    {
        if (Physics.SphereCast(distanceSensor.position, ultrasonicRadius, distanceSensor.forward, out var hitInfo, 10f))
        {
            return hitInfo.distance * 100f;
        }

        return float.PositiveInfinity;
    }
}