using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Door : MonoBehaviour
{
    [SerializeField] private GameObject physicalDoor;

    private void OnEnable()
    {
        SC_Button.monumentClicked += Open;
    }

    private void OnDisable()
    {
        SC_Button.monumentClicked -= Open;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        physicalDoor.SetActive(false);
        Debug.Log("whaaa");
    }

    public void Close()
    {
        physicalDoor.SetActive(true);
    }
}
