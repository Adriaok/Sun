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
        //Ignore collision between default layer & lightupobjects layer
        Physics.IgnoreLayerCollision(0, 6, true);

        //Ignore collision with other lightupobjects
        Physics.IgnoreLayerCollision(6, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        shadow.SetActive(false);
        Physics.IgnoreCollision(collider, playerCollider, true);
        Physics.IgnoreLayerCollision(6, 7);     //Doesn't worK
    }

    public void LightDown()
    {
        shadow.SetActive(true);
        Physics.IgnoreCollision(collider, playerCollider, false);
        Physics.IgnoreLayerCollision(6, 7, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter shadow");
        SC_FaithSystem.Instance.UpdateTotalFear(10);


        SC_Flashlight flashlight = other.GetComponentInParent<SC_Flashlight>();
        if(flashlight != null)
        {
            SC_UI_MessageManager.Instance.ShowMessage("A follower has been swallowed by the shadows");
            //Call destroy flashlight function
            flashlight.GetComponent<SC_Follower>().Die();
            //Increase total fear
            //Decrease total faith
        }
    }
}
