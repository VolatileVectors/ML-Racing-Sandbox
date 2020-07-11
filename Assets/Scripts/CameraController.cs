using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float freeLookSensitivity = 3f;

    protected bool Looking;

    private void StartLooking()
    {
        Looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void StopLooking()
    {
        Looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }
    }

    protected virtual void OnDisable()
    {
        StopLooking();
    }

    protected static void DrawBounds(Bounds bounds)
    {
        Gizmos.color = Color.red;
        
        // bottom
        var p1 = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        var p2 = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        var p3 = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        var p4 = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);

        // top
        var p5 = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        var p6 = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        var p7 = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        var p8 = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);

        Gizmos.DrawLine(p5, p6);
        Gizmos.DrawLine(p6, p7);
        Gizmos.DrawLine(p7, p8);
        Gizmos.DrawLine(p8, p5);

        // sides
        Gizmos.DrawLine(p1, p5);
        Gizmos.DrawLine(p2, p6);
        Gizmos.DrawLine(p3, p7);
        Gizmos.DrawLine(p4, p8);
    }
}