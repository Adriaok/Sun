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

    public bool isInPlayerFaction = false;

    private SC_UI_FollowerActions followerActions;

    [SerializeField]
    private GameObject panelUI;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        followerActions = panelUI.GetComponent<SC_UI_FollowerActions>();
        panelUI.SetActive(false);
    }

    void Update()
    {
        CheckIfSelected();

        if (isInPlayerFaction)
        {
            CheckIfDragging();
            CheckIfRotating();
            CheckIfRotationLock();
            CheckIfToggleLight();
            CheckIfThrowing();
        }
        else
        {
            CheckIfRecruiting();
        }

        UpdateMaterialColor();
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        panelUI.SetActive(true);
    }

    private void OnMouseExit()
    {
        isHovered = false;

        if(!isSelected)
            panelUI.SetActive(false);
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

    private void CheckIfRecruiting()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.R))
        {
            isInPlayerFaction = true;
            followerActions.actionObjectsWithID["Action_Recruit"].SetActive(false);
            BroadcastMessage("UpdateIsInPlayerFaction_SC_Follower", true);
            Debug.Log("Recruit");
            isSelected = false;
            BroadcastMessage("UpdateIsSelected_SC_Follower", false);

            followerActions.actionObjectsWithID["Action_Select"].SetActive(true);
            followerActions.actionObjectsWithID["Action_Deselect"].SetActive(false);

            //Init options (according to default bool values)
            followerActions.actionObjectsWithID["Action_Drag"].SetActive(true);
            followerActions.actionObjectsWithID["Action_Lock"].SetActive(true);
            followerActions.actionObjectsWithID["Action_EnableLight"].SetActive(true);
            followerActions.actionObjectsWithID["Action_RotateLeft"].SetActive(true);
            followerActions.actionObjectsWithID["Action_RotateRight"].SetActive(true);
            followerActions.actionObjectsWithID["Action_LockRotation"].SetActive(true);
        }
    }
    private void CheckIfSelected()
    {
        if (isHovered && Input.GetMouseButtonDown(0))
        {
            if (isSelected)
            {
                isSelected = false;
                BroadcastMessage("UpdateIsSelected_SC_Follower", false);
                panelUI.SetActive(false);

                followerActions.actionObjectsWithID["Action_Select"].SetActive(true);
                followerActions.actionObjectsWithID["Action_Deselect"].SetActive(false);
            }
            else
            {
                isSelected = true;
                BroadcastMessage("UpdateIsSelected_SC_Follower", true);

                followerActions.actionObjectsWithID["Action_Select"].SetActive(false);
                followerActions.actionObjectsWithID["Action_Deselect"].SetActive(true);

                if(!isInPlayerFaction)
                {
                    followerActions.actionObjectsWithID["Action_Recruit"].SetActive(true);
                }
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

    private void CheckIfRotationLock()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.M))
        {
            BroadcastMessage("LockRotation_SC_Follower");
            isSelected = false;
            BroadcastMessage("UpdateIsSelected_SC_Follower", false);

            followerActions.actionObjectsWithID["Action_Select"].SetActive(true);
            followerActions.actionObjectsWithID["Action_Deselect"].SetActive(false);

            followerActions.actionObjectsWithID["Action_LockRotation"].active = !followerActions.actionObjectsWithID["Action_LockRotation"].active;
            followerActions.actionObjectsWithID["Action_UnlockRotation"].active = !followerActions.actionObjectsWithID["Action_UnlockRotation"].active;
        }
    }

    private void CheckIfToggleLight()
    {
        if (isSelected && Input.GetKeyDown(KeyCode.L))
        {
            BroadcastMessage("ToggleLight_SC_Follower");
            isSelected = false;
            BroadcastMessage("UpdateIsSelected_SC_Follower", false);

            followerActions.actionObjectsWithID["Action_Select"].SetActive(true);
            followerActions.actionObjectsWithID["Action_Deselect"].SetActive(false);

            followerActions.actionObjectsWithID["Action_EnableLight"].active = !followerActions.actionObjectsWithID["Action_LockRotation"].active;
            followerActions.actionObjectsWithID["Action_DisableLight"].active = !followerActions.actionObjectsWithID["Action_UnlockRotation"].active;
        }
    }

    private void CheckIfThrowing()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.Q))
        {
            BroadcastMessage("UpdateIsThrowing_SC_Follower");
        }
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
