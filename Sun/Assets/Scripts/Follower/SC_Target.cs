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
        CheckIfRotating();
        CheckIfToggleLight();

        UpdateMaterialColor();
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
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
                BroadcastMessage("UpdateIsSelected_SC_Follower", false);
            }
            else
            {
                isSelected = true;
                BroadcastMessage("UpdateIsSelected_SC_Follower", true);
            }
       
        
        }
    }

    private void CheckIfRotating()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.Z))
        {
            BroadcastMessage("Rotate_SC_Follower", -10.0f);
        }

        if (isSelected && Input.GetKeyDown(KeyCode.X))
        {
            BroadcastMessage("Rotate_SC_Follower", 10.0f);
        }
    }

    private void CheckIfToggleLight()
    {
        if (isSelected && Input.GetKeyDown(KeyCode.L))
            BroadcastMessage("ToggleLight_SC_Follower");
    }

    private void UpdateMaterialColor()
    {
        if (isHovered && !isSelected)
            renderer.material.color = Color.blue;

        else if (isSelected)
            renderer.material.color = Color.green;

        else
            renderer.material.color = Color.white;
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
