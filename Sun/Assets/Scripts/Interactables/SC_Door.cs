using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Door : MonoBehaviour
{
    [SerializeField] private GameObject physicalDoor;

    public void Open()
    {
        physicalDoor.SetActive(false);
    }

    public void Close()
    {
        physicalDoor.SetActive(true);
    }
}
