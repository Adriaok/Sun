using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float speed = .5f;

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = _virtualCamera.transform.forward;
        Vector3 right = _virtualCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 relativeForward = forward * verticalInput;
        Vector3 relativeRight = right * horizontalInput;

        Vector3 relativeMovement = (relativeForward + relativeRight) * speed;
        transform.Translate(relativeMovement, Space.World);
    }
}
