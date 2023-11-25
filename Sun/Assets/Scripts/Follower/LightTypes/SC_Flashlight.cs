using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SC_Flashlight : MonoBehaviour
{
    private Light light;
    private LightUpObject foundObject;
    private string raycastReturn;
    private bool isConsumed = false;
    private float timeSinceBirth = 0.0f;
    private bool isLightToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        light.type = UnityEngine.LightType.Spot;
        light.color = Color.blue;
        light.intensity = 0.5f;
        light.range = 1000.0f;
        light.enabled = false;
    }

    private void FixedUpdate()
    {
        if (isLightToggled)
        {
            CheckRayCastCollision();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isConsumed && isLightToggled)
        {
            if (CheckIfConsumed())
            {
                ToggleLight(false);
                isConsumed = true;
                Debug.Log("is consumed");
            }
        }
    }

    public void CheckRayCastCollision()
    {
        // Bit shift the index of the layer (6) to get a bit mask
        // This would cast rays only against colliders in layer 6.
        int layerMask = 1 << 6;

        RaycastHit hit;
        if (Physics.Raycast(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        {
            Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                foundObject = hit.collider.gameObject.GetComponent<LightUpObject>();
                foundObject.LightUp();
            }
        }
        else
        {
            Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * 1000, Color.black);

            if (foundObject != null)
            {
                foundObject.LightDown();
            }
        }
    }

    private bool CheckIfConsumed()
    {
        timeSinceBirth += Time.deltaTime;

        if (timeSinceBirth > 60.0f)
            return true;
        else
            return false;
    }

    public void ToggleLight(bool _state)
    {
        isLightToggled = _state;
        light.enabled = _state;

        if (!_state)
        {
            int layerMask = 1 << 6;

            RaycastHit[] hits = Physics.SphereCastAll(GetComponent<Rigidbody>().position, light.range, transform.TransformDirection(Vector3.forward), light.range, layerMask);
            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    foundObject = hit.collider.gameObject.GetComponent<LightUpObject>();
                    foundObject.LightDown();
                }
            }
        }
    }
}
