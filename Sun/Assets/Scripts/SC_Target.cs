using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Target : MonoBehaviour
{
    private MeshRenderer renderer;
    private bool isHovered = false;
    public bool isSelected = false;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        CheckIfSelected();
    }

    private void OnMouseEnter()
    {
        Debug.Log("On enter");
        renderer.material.color = Color.blue;
        isHovered = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("On exit");
        if (!isSelected)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            renderer.material.color = Color.green;
        }
        isHovered = false;
    }

    private void CheckIfSelected()
    {
        if (isHovered && Input.GetMouseButtonDown(0) /*&& Input.GetKeyDown(KeyCode.Alpha1)*/)
        {
            if (isSelected)
            {
                isSelected = false;
                renderer.material.color = Color.blue;
                Debug.Log("Deselect");
            }
            else
            {
                isSelected = true;
                renderer.material.color = Color.green;
                Debug.Log("Select");
            }
        }
    }
}
