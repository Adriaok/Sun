using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SC_Button : MonoBehaviour
{
    [SerializeField] private SC_Door door;
    Collider triggeringCollider = null;

    private void Start()
    {
        //door = GetComponentInChildren<SC_Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggeringCollider == null)
        {
            door.Open();
            triggeringCollider = other;
            ChangeIsAfraidFollower(triggeringCollider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == triggeringCollider)
        {
            door.Close();
            ChangeIsAfraidFollower(triggeringCollider);
            triggeringCollider = null;
        }
    }

    private void ChangeIsAfraidFollower(Collider other)
    {
        SC_Follower follower = other.GetComponent<SC_Follower>();
        if (follower != null)
            follower.ChangeIsAfraid();
    }
}
