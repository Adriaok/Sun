using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SC_Button : MonoBehaviour
{
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

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("collided");
            if (monumentClicked != null)
            {
                monumentClicked();
            }
        }


        if (collision.gameObject.name == "/Player/Player/Capsule")
        {
        }
    }
    */
    private void OnTriggerEnter()
    {
        if (monumentClicked != null)
            monumentClicked();
    }
}
