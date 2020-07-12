using UnityEngine;

public class MotorController : MonoBehaviour
{
    [Header("References")] public Transform wheelPivot;
    public HingeJoint wheelJoint;

    private JointMotor _motor;
    private bool _invertTorque = false;

    private void Awake()
    {
        _motor = wheelJoint.motor;
        var euler = transform.localEulerAngles;
        if (Mathf.Approximately(euler.y, 180f)) _invertTorque = !_invertTorque;
        if (Mathf.Approximately(euler.z, 180f)) _invertTorque = !_invertTorque;
    }

    void Update()
    {
        wheelPivot.rotation = Quaternion.AngleAxis(wheelJoint.angle, wheelPivot.right) * wheelPivot.rotation;
    }

    public void SetTorque(float torque)
    {
        _motor.targetVelocity = _invertTorque ? -torque : torque;
        wheelJoint.motor = _motor;
    }
}