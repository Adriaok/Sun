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

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        GetComponentInParent<Light>().type = UnityEngine.LightType.Spot;
        GetComponentInParent<Light>().color = Color.blue;
        GetComponentInParent<Light>().intensity = 0.5f;
        GetComponentInParent<Light>().range = 1000.0f;
    }

    private void FixedUpdate()
    {
        CheckRayCastCollision();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isConsumed)
        {
            if (CheckIfConsumed())
            {
                light.enabled = false;
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
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        {
            Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                //raycastReturn = hit.collider.gameObject.name;
                //foundObject = GameObject.Find(raycastReturn);
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
}
