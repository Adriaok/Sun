using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_Button : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        OpenDoor();
    }

    private void OnCollisionExit(Collision collision)
    {
        CloseDoor();
    }

    public void OpenDoor()
    {
        door.GetComponent<SC_Door>().Open();
    }

    public void CloseDoor()
    {
        door.GetComponent<SC_Door>().Close();
    }
}
