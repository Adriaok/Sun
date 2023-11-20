using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    CinemachineOrbitalTransposer _transposer;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.Normalize(new Vector3(_transposer.m_XAxis.Value, 0, 180)));
            Vector3 right = transform.TransformDirection(Vector3.Normalize(new Vector3(180, 0, _transposer.m_XAxis.Value)));

            float curSpeedX = speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
