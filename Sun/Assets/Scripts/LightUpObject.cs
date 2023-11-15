using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    public GameObject shadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        shadow.SetActive(false);
        //GetComponent<Collider>().enabled = false;
    }

    public void LightDown()
    {
        shadow.SetActive(true);
        //GetComponent<Collider>().enabled = true;
    }
}
