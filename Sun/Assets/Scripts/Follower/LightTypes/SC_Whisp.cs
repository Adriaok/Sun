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

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        GetComponentInParent<Light>().type = UnityEngine.LightType.Spot;
        GetComponentInParent<Light>().color = Color.red;
        GetComponentInParent<Light>().intensity = 1.0f;
        GetComponentInParent<Light>().range = 500.0f;
    }

    private void FixedUpdate()
    {
        CheckRayCastCollision();
    }
    // Update is called once per frame
    void Update()
    {
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
                    foundObject.LightUp();
                else
                    foundObject.LightDown();
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

}
