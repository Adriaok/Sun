using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] public PlayerController player;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector2 rotation = Vector2.zero;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        rotation.y = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        player.transform.eulerAngles = new Vector2(0, rotation.y);
    }
}
