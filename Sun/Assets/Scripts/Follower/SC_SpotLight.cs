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
    private GameObject foundObject;
    private string raycastReturn;

    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponentInParent<Light>();

        GetComponentInParent<Light>().type = UnityEngine.LightType.Spot;

        GetComponentInParent<Light>().color = Color.green;
        Debug.Log("Color changed to green");
    }

    private void FixedUpdate()
    {
        CheckRayCastCollision();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void CheckRayCastCollision()
    {
        // Bit shift the index of the layer (6) to get a bit mask
        // This would cast rays only against colliders in layer 6.
        int layerMask = 1 << 6;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, spotLight.range, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                raycastReturn = hit.collider.gameObject.name;
                foundObject = GameObject.Find(raycastReturn);
                Destroy(foundObject);
                Debug.Log("did hit");
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.black);
            Debug.Log("Did not Hit");
        }
    }
}
