using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    public GameObject shadow;
    private Collider playerCollider;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        shadow.SetActive(false);
        //GetComponent<Collider>().enabled = false;
        Physics.IgnoreCollision(collider, playerCollider, true);
        //Physics.IgnoreLayerCollision(0, 7);     //Doesn't worK
        Physics.IgnoreLayerCollision(6, 7);     //Doesn't worK
    }

    public void LightDown()
    {
        shadow.SetActive(true);
        //GetComponent<Collider>().enabled = true;
        Physics.IgnoreCollision(collider, playerCollider, false);
        //Physics.IgnoreLayerCollision(0, 7, false);
        Physics.IgnoreLayerCollision(6, 7, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter shadow");
    }
}
