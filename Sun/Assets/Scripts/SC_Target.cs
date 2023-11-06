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
        renderer.material.color = Color.blue;
        isHovered = true;
    }

    private void OnMouseExit()
    {
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
        if (isHovered && Input.GetMouseButtonDown(0))
        {
            if (isSelected)
            {
                isSelected = false;
                renderer.material.color = Color.blue;
                BroadcastMessage("UpdateIsSelected_SC_Follower", false);
            }
            else
            {
                isSelected = true;
                renderer.material.color = Color.green;
                BroadcastMessage("UpdateIsSelected_SC_Follower", true);
            }
        }
    }

    public void UpdateIsSelected_SC_Target(bool _isSelected)
    {
        isSelected = _isSelected;
    }
}
