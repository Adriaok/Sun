using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] float rotationSpeed;


    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        rotationSpeed = 100f;
    }

    // Update is called once per frame
    void Update()
    {


        //TODO: when player cursor moves in a direction camera damps that way
        //TODO: try to contain all followers in camera 
        //TODO: righ click + mouse to move

        if (Input.GetMouseButtonDown(1))
        {
            float rotY = rotationSpeed * Input.GetAxis("Mouse X");
            float rotZ = rotationSpeed * Input.GetAxis("Mouse Y");
            //transform.rotation = Quaternion.AngleAxis(0f, 0f, 0f);

            transform.rotation = Quaternion.Euler(40f, rotY, rotZ);

        }
        Debug.Log(Input.GetAxis("Mouse X"));
        //Console.WriteLine();
        //Console.WriteLine(Input.GetAxis("Mouse Y"));
        //Console.WriteLine(Input.GetAxis("Mouse Y"));

    }
}
