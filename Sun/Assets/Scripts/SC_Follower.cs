using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_Follower : MonoBehaviour
{    
    public Transform transformToFollow;
    NavMeshAgent agent;

    public bool isLocked = false;
    public bool isSelected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        transformToFollow = GameObject.Find("Player").transform;
    }

    public void FollowPlayer()
    {
        //Follow the player
        agent.destination = transformToFollow.position;
    }

    public void UpdateIsSelected_SC_Follower(bool _isSelected)
    {
        isSelected = _isSelected;
    }

    public void UpdateIsSelectedAndBroadcast(bool _isSelected)
    {
        UpdateIsSelected_SC_Follower(_isSelected);
        BroadcastMessage("UpdateIsSelected_SC_Target", _isSelected);
    }
}
