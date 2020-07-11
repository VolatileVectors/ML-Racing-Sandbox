using UnityEngine;

/// Free camera controls
/// 
/// Keys:
///	w/a/s/d / arrows	  - movement
///	r/f / pageup/pagedown - up/down
///	right mouse  	      - enable free look
///	mouse			      - free look / rotation
public class FreeCameraController : CameraController
{
    public float movementSpeed = 5f;
    public float movementAcceleration = 2.5f;
    public Bounds cameraBounds;

    private float _currentMovementSpeed;
    private Vector3 _movementDelta;

    protected override void Update()
    {
        base.Update();

        var targetDelta = Vector3.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            targetDelta -= transform.right;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetDelta += transform.right;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            targetDelta += transform.forward;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            targetDelta -= transform.forward;
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            targetDelta += Vector3.up;
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            targetDelta -= Vector3.up;
        }

        if (targetDelta == Vector3.zero)
        {
            _currentMovementSpeed -= 2f * movementAcceleration * Time.deltaTime;
        }
        else
        {
            _currentMovementSpeed += movementAcceleration * Time.deltaTime;
            _movementDelta += targetDelta;
            _movementDelta /= 2f;
            _movementDelta.Normalize();
        }

        var linearHeight = Mathf.InverseLerp(cameraBounds.min.y, cameraBounds.max.y, transform.position.y);
        var speedHeightMultiplier = 0.4f + linearHeight * linearHeight * 0.6f;
        _currentMovementSpeed = Mathf.Clamp(_currentMovementSpeed, 0f, movementSpeed * speedHeightMultiplier);
        if (Mathf.Approximately(_currentMovementSpeed, 0f))
        {
            _movementDelta = Vector3.zero;
        }

        var pos = transform.position;
        pos += _movementDelta * (_currentMovementSpeed * Time.deltaTime);
        if (!cameraBounds.Contains(pos))
        {
            pos = cameraBounds.ClosestPoint(pos);
        }

        transform.position = pos;

        if (Looking)
        {
            transform.rotation =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * freeLookSensitivity, Vector3.up) *
                Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * freeLookSensitivity, transform.right) *
                transform.rotation;
        }
    }

    private void OnDrawGizmosSelected()
    {
        DrawBounds(cameraBounds);
    }
}