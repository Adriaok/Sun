using UnityEngine;
using System.Collections;

public class SC_Button : MonoBehaviour
{
    public GameObject door;

    public delegate void MonumentActions();
    public static event MonumentActions monumentClicked;

    [SerializeField] private PlayerController player;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 2.5f && monumentClicked != null)
                monumentClicked();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "/Player/Player/Capsule")
        {
            Debug.Log("collided");
            if (monumentClicked != null)
            {
                monumentClicked();
            }
        }
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
