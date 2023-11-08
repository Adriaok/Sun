using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        //TODO: when player cursor moves in a direction camera damps that way
        //TODO: try to contain all followers in camera 
        //TODO: righ click + mouse to move

        if (Input.GetMouseButtonDown(1))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                
            Input.GetAxis("Mouse X");
            Input.GetAxis("Mouse Y");
        }


    }
}
