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

    private void Awake()
    {
        cameraOldPos = virtualCamera.transform.position;
        transform.position = new Vector3(cameraOldPos.x, cameraOldPos.y - 2, transform.position.z);
    }

    void FixedUpdate()
    { 
        cameraPosDiff = (Vector2)virtualCamera.transform.position - cameraOldPos;
        transform.position += new Vector3(cameraPosDiff.x *speed  , cameraPosDiff.y);
        cameraOldPos = virtualCamera.transform.position;
    }
}
