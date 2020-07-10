using UnityEngine;

public class CarController : MonoBehaviour
{
    [Range(-4096f, 4096f)] public float leftUpperSpeed;
    [Range(-4096f, 4096f)] public float leftLowerSpeed;
    [Range(-4096f, 4096f)] public float rightUpperSpeed;
    [Range(-4096f, 4096f)] public float rightLowerSpeed;
    public float maxMotorSpeed = 5f;

    [Header("References")] public HeadController head;
    public LineTrackingController lineTracking;
    public WheelCollider leftUpper;
    public WheelCollider leftLower;
    public WheelCollider rightUpper;
    public WheelCollider rightLower;

    private void Update()
    {
        leftUpper.motorTorque = leftUpperSpeed / 4096f * maxMotorSpeed;
        leftLower.motorTorque = leftLowerSpeed / 4096f * maxMotorSpeed;
        rightUpper.motorTorque = rightUpperSpeed / 4096f * maxMotorSpeed;
        rightLower.motorTorque = rightLowerSpeed / 4096f * maxMotorSpeed;
    }

    public void PWM_setMotorModel(float leftUp, float leftLow, float rightUp, float rightLow)
    {
        leftUpperSpeed = leftUp;
        leftLowerSpeed = leftLow;
        rightUpperSpeed = rightUp;
        rightLowerSpeed = rightLow;
    }

    public void PWM_setServoPwm(char servo, float angle)
    {
        switch (servo)
        {
            case '0':
                head.swivel = angle;
                break;
            case '1':
                head.tilt = angle;
                break;
        }
    }

    public float Ultrasonic_get_distance()
    {
        return head.GetDistance();
    }

    public RenderTexture Camera_read()
    {
        return head.carCamera.targetTexture;
    }

    public bool GPIO_input(string io)
    {
        switch (io)
        {
            case "IR01":
                return lineTracking.GetInfraRed(LineTrackingController.InfraRed.Left);
            case "IR02":
                return lineTracking.GetInfraRed(LineTrackingController.InfraRed.Middle);
            case "IR03":
                return lineTracking.GetInfraRed(LineTrackingController.InfraRed.Right);
        }

        return false;
    }

    public void SetLeftUpperSpeed(float value)
    {
        leftUpperSpeed = value;
    }

    public void SetLeftLowerSpeed(float value)
    {
        leftLowerSpeed = value;
    }

    public void SetRightUpperSpeed(float value)
    {
        rightUpperSpeed = value;
    }

    public void SetRightLowerSpeed(float value)
    {
        rightLowerSpeed = value;
    }

    public void SetSwivel(float value)
    {
        head.swivel = value;
    }

    public void SetTilt(float value)
    {
        head.tilt = value;
    }
}