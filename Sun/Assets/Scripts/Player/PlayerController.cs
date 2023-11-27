using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

    CharacterController controller;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float speed = .5f;
    [SerializeField] private float gravity = 20.5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

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
        relativeMovement.y -= gravity;
        controller.Move(relativeMovement);
    }
}
