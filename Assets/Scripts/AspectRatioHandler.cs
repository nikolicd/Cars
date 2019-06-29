using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the camera orthographic size based on a phones resolution
/// </summary>
public class AspectRatioHandler : MonoBehaviour
{
    float width = 1125;
    float height = 2436;

    void Start()
    {
        Camera.main.orthographicSize = (Camera.main.orthographicSize * Screen.height / Screen.width) / (height / width);
    }
}
