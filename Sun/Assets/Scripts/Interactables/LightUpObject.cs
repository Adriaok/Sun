using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    public GameObject shadow;
    private Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        shadow.SetActive(false);
        //GetComponent<Collider>().enabled = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), playerCollider, true);
        Physics.IgnoreLayerCollision(7, 8, true);
    }

    public void LightDown()
    {
        shadow.SetActive(true);
        //GetComponent<Collider>().enabled = true;
        Physics.IgnoreCollision(GetComponent<Collider>(), playerCollider, false);
        Physics.IgnoreLayerCollision(7, 8, false);
    }
}
