using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalLightController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private new readonly Light light;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            light.type = LightType.Area;
            light.intensity = 1;
            light.transform.position = playerController.transform.position;
            light.AddComponent<Light>();
        }
        
    }
}
