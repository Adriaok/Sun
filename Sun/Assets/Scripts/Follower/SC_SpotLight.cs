using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//UnityEngineLight
//NO FER SERVIR EXPERIMENTAL

//Raycast de spot light per detecar si quelcom està il·luminat (llargada)

public class SC_SpotLight : MonoBehaviour
{
    private Light spotLight;

    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponentInParent<Light>();

        GetComponentInParent<Light>().type = UnityEngine.LightType.Spot;

        GetComponentInParent<Light>().color = Color.green;
        Debug.Log("Color changed to green");
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayCastCollision();
    }

    public void CheckRayCastCollision()
    {
        Debug.Log("Raycast range: " + spotLight.range);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, spotLight.range))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.black);
            Debug.Log("Did not Hit");
        }
    }
}
