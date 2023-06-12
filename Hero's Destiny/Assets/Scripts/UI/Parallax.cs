using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase virtualCamera;
    [SerializeField] private float speed;
    private Vector2 cameraOldPos;
    private Vector2 cameraPosDiff;

    private void Start()
    {
        cameraOldPos = virtualCamera.transform.position;
    }

    void FixedUpdate()
    { 
        cameraPosDiff = (Vector2)virtualCamera.transform.position - cameraOldPos;
        transform.position += new Vector3(cameraPosDiff.x *speed  , cameraPosDiff.y *.6f);
        cameraOldPos = virtualCamera.transform.position;
    }
}
