using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SC_Button : MonoBehaviour
{
    [SerializeField] private SC_Door door;

    private void Start()
    {
        //door = GetComponentInChildren<SC_Door>();
    }

    private void OnTriggerEnter()
    {
        door.Open();
    }

    private void OnTriggerExit()
    {
        door.Close();
    }
}
