using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//https://docs.unity3d.com/ScriptReference/Experimental.GlobalIllumination.SpotLight.html
//https://docs.unity3d.com/ScriptReference/Experimental.GlobalIllumination.LinearColor.html

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
        //light.instanceID = ???? -> same ID as follower? or the same ID adding  "light_" or "spotlight_"
        light.mode = LightMode.Realtime;
        //light.orientation = -> follower's orientation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
