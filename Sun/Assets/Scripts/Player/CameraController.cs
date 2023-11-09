using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    CinemachineOrbitalTransposer _transposer;
    private bool canTurn;


    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        _transposer.m_XAxis.m_InputAxisName = null;
        canTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            canTurn = true;

        if (canTurn)
        {
            if (_transposer.m_XAxis.Value != 0)
                _transposer.m_RecenterToTargetHeading.m_enabled = true;
            else
            {
                _transposer.m_RecenterToTargetHeading.m_enabled = false;
                canTurn = false;
            }
        }

        if (Input.GetMouseButton(1))
            _transposer.m_XAxis.m_InputAxisName = "Mouse X";
        else
            _transposer.m_XAxis.m_InputAxisName = null;
    }
}
