using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SC_Whisp : MonoBehaviour
{
    private Light light;
    private LightUpObject foundObject;
    private string raycastReturn;
    private float timeSinceChange = 0.0f;
    private bool isLightToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        light.type = UnityEngine.LightType.Spot;
        light.color = Color.red;
        light.intensity = 1.0f;
        light.range = 500.0f;
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
        if(isLightToggled)
            FlashLightAtInterval();
    }

    public void CheckRayCastCollision()
    {
        // Bit shift the index of the layer (6) to get a bit mask
        // This would cast rays only against colliders in layer 6.
        int layerMask = 1 << 6;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        {
            Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                //raycastReturn = hit.collider.gameObject.name;
                //foundObject = GameObject.Find(raycastReturn);
                foundObject = hit.collider.gameObject.GetComponent<LightUpObject>();
                if (light.enabled)
                {
                    foundObject.LightUp();
                    Debug.Log("Light up");
                }
                else
                {
                    foundObject.LightDown();
                    Debug.Log("Light down");
                }
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

    public void FlashLightAtInterval()
    {
        timeSinceChange += Time.deltaTime;
        if (timeSinceChange > 1.0f)
        {
            timeSinceChange = 0.0f;
            light.enabled = !light.enabled;
        }
    }

    public void ToggleLight(bool _state)
    {
        isLightToggled = _state;
        light.enabled = _state;
    }

}
