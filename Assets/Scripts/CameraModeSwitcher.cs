using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    public List<CameraController> cameras;

    public void SetCamera(int index)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].enabled = i == index;
        }
    }
}