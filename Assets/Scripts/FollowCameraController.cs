using UnityEngine;

/// Follow camera controls
/// 
/// Keys:
///	right mouse    - enable free look
///	mouse		   - free look / rotation
/// mouse wheel    - zoom
public class FollowCameraController : CameraController
{
    public float zoomSensitivity = 3f;
    public float minZoomDistance = 0.25f;
    public float maxZoomDistance = 3f;
    public float minPitch = 10f;
    public float maxPitch = 87.5f;
    public Transform followTarget;

    private Vector3 _oldFollowTargetPos;

    private void OnEnable()
    {
        _oldFollowTargetPos = followTarget.position;
        var rot = transform.rotation;
        rot.SetLookRotation(followTarget.position - transform.position, Vector3.up);
        transform.rotation = rot;

        var pos = transform.position;
        pos += ApplyZoom(0f);
        transform.position = pos;
    }

    protected override void Update()
    {
        base.Update();

        var movementDelta = followTarget.position - _oldFollowTargetPos;
        _oldFollowTargetPos = followTarget.position;

        var axis = Input.GetAxis("Mouse ScrollWheel");
        if (!Mathf.Approximately(axis, 0f))
        {
            movementDelta += ApplyZoom(-axis);
        }

        var pos = transform.position;
        pos += movementDelta;
        transform.position = pos;

        if (Looking)
        {
            transform.RotateAround(followTarget.position, Vector3.up, Input.GetAxis("Mouse X") * freeLookSensitivity);
            var eulerX = transform.localEulerAngles.x;
            var targetEulerX = Mathf.Clamp(eulerX - Input.GetAxis("Mouse Y") * freeLookSensitivity, minPitch, maxPitch);
            transform.RotateAround(followTarget.position, transform.right, targetEulerX - eulerX);
        }
    }

    private Vector3 ApplyZoom(float axis)
    {
        var positionDelta = followTarget.position - transform.position;
        var distance = positionDelta.magnitude;
        positionDelta.Normalize();
        var targetDistance = Mathf.Clamp(distance + axis * zoomSensitivity, minZoomDistance, maxZoomDistance);
        return positionDelta * (distance - targetDistance);
    }
}