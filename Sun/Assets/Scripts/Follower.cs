using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{    
    public Transform transformToFollow;
    NavMeshAgent agent;

    public bool isLocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void FollowPlayer()
    {
        //Follow the player
        agent.destination = transformToFollow.position;
    }
}
