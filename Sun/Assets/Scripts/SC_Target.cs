using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Target : MonoBehaviour
{
    private MeshRenderer renderer;
    private bool isHovered = false;
    public bool isSelected = false;

    private bool isDragging = false;
    private float distance;
    private Vector3 startDist;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        CheckIfSelected();
        CheckIfDragging();
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

    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        isDragging = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;
        BroadcastMessage("UpdateIsDragging_SC_Follower", true);

    }

    private void OnMouseUp()
    {
        isDragging = false;
        BroadcastMessage("UpdateIsDragging_SC_Follower", false);
        
        //Code to deselect the follower when dropping it 
        //isSelected = false;
        //BroadcastMessage("UpdateIsSelected_SC_Follower", false);
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

    private void CheckIfDragging()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint + startDist;
        }
    }

    public void UpdateIsSelected_SC_Target(bool _isSelected)
    {
        isSelected = _isSelected;
    }
}
