using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//UnityEngineLight
//NO FER SERVIR EXPERIMENTAL

//Raycast de spot light per detecar si quelcom està il·luminat (llargada)

public class SC_SpotLight : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Light>().type = UnityEngine.LightType.Spot;

        GetComponentInParent<Light>().color = Color.green;
        Debug.Log("Color changed to green");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
