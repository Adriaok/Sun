using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SC_Lantern : MonoBehaviour
{
    private Light light;
    private LightUpObject foundObject;
    private string raycastReturn;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        GetComponentInParent<Light>().type = UnityEngine.LightType.Point;
        GetComponentInParent<Light>().color = Color.green;
        GetComponentInParent<Light>().intensity = 0.5f;
        GetComponentInParent<Light>().range = 200.0f;
    }

    private void FixedUpdate()
    {
        CheckSphereCastCollision();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void CheckSphereCastCollision()
    {
        // Bit shift the index of the layer (6) to get a bit mask
        // This would cast rays only against colliders in layer 6.
        int layerMask = 1 << 6;

        RaycastHit hit;
        // Does the sphere intersect any objects excluding the player layer
        //if (Physics.Raycast(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        if (Physics.SphereCast(GetComponent<Rigidbody>().position, light.range, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        {
            Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                Debug.Log("Sphere cast detected obj");
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
}
