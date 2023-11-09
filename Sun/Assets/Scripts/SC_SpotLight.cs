using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//https://docs.unity3d.com/ScriptReference/Experimental.GlobalIllumination.SpotLight.html
//https://docs.unity3d.com/ScriptReference/Experimental.GlobalIllumination.LinearColor.html
//https://catlikecoding.com/unity/tutorials/custom-srp/point-and-spot-lights/

public class SC_SpotLight : MonoBehaviour
{
    private SpotLight light; 
    
    // Start is called before the first frame update
    void Start()
    {
        light.angularFalloff = AngularFalloffType.LUT;
        light.color = LinearColor.Convert(Color.white, 1.0f);
        light.coneAngle = 45.0f;
        light.falloff = FalloffType.Linear;
        light.indirectColor = LinearColor.Convert(Color.white, 1.0f);
        light.innerConeAngle = 30.0f;
        //TODO: Refactor follower id to int? 
        //light.instanceID =  GetComponent<SC_Follower>().ID;
        light.mode = LightMode.Realtime;
        light.orientation = GetComponent<SC_Follower>().transform.rotation;
        light.position = GetComponent<SC_Follower>().transform.position;
        light.range = GetComponent<SC_Follower>().lightRange;
        light.shadow = true;
        light.sphereRadius = 5.0f;  //TODO: Check if this value makes sense
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
