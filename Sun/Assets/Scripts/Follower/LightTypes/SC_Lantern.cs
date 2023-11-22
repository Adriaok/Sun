using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SC_Lantern : MonoBehaviour
{
    private Light light;
    private LightUpObject foundObject;
    private string raycastReturn;
    private bool isLightToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInParent<Light>();

        light.type = UnityEngine.LightType.Point;
        light.color = Color.green;
        light.intensity = 0.5f;
        light.range = 200.0f;
        light.enabled = false;
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

        RaycastHit[] hits = Physics.SphereCastAll(GetComponent<Rigidbody>().position, light.range, transform.TransformDirection(Vector3.forward), light.range, layerMask);
        //if (Physics.SphereCast(GetComponent<Rigidbody>().position, light.range, transform.TransformDirection(Vector3.forward), out hit, light.range, layerMask))
        foreach(var hit in hits)
        {
            //Debug.DrawRay(GetComponent<Rigidbody>().position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            if (hit.collider != null)
            {
                foundObject = hit.collider.gameObject.GetComponent<LightUpObject>();
                if (light.enabled)
                {
                    foundObject.LightUp();
                    Debug.Log(foundObject.name);
                }
                else
                {
                    foundObject.LightDown();
                }
            }
        }
    
    }

    public void ToggleLight(bool _state)
    {
        isLightToggled = _state;
        light.enabled = _state;
    }
}
