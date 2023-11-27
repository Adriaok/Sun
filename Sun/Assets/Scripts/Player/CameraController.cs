using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;
    public CinemachineOrbitalTransposer _transposer;

    private bool canTurn;
    private float cameraDistance;
    [SerializeField] private float zoomSensitivity;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        _transposer.m_XAxis.m_InputAxisName = null;

        canTurn = false;
        cameraDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            canTurn = true;

        if (canTurn)
        {
            if (_transposer.m_XAxis.Value <= 1 && _transposer.m_XAxis.Value >= -1)
            {
                _transposer.m_RecenterToTargetHeading.m_enabled = false;
                canTurn = false;
            }
            else
            {
                _transposer.m_RecenterToTargetHeading.m_enabled = true;
            }
        }

        if (Input.GetMouseButton(1))
            _transposer.m_XAxis.m_InputAxisName = "Mouse X";
        else
            _transposer.m_XAxis.m_InputAxisName = null;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            //Debug.Log(cameraDistance);

            _transposer.m_FollowOffset.y = Mathf.Lerp(_transposer.m_FollowOffset.y, _transposer.m_FollowOffset.y - cameraDistance, 5f);
        }
    }
}
