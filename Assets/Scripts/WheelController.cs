using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("References")] public Transform wheelPivot;

    private WheelCollider _wheel;

    void Start()
    {
        _wheel = GetComponent<WheelCollider>();
        _wheel.ConfigureVehicleSubsteps(5, 12, 15); //TODO fix me
    }

    void Update()
    {
        _wheel.GetWorldPose(out var pos, out var quat);
        wheelPivot.position = pos;
        wheelPivot.rotation = quat;
    }
}