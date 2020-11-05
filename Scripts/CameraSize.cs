using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSize : MonoBehaviour
{
    private float _startAspect = 1080f / 1920f;

    private float _defaultHeight;
    private float _defaultWidth;

    private void Awake()
    {
        _defaultHeight = Camera.main.orthographicSize;
        _defaultWidth = Camera.main.orthographicSize * _startAspect;

        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }
}
