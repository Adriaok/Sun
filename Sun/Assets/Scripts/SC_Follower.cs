using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_Follower : MonoBehaviour
{
    //public Transform transformToFollow;
    //NavMeshAgent agent;

    [SerializeField] private float minDistance = 3.0f;
    [SerializeField] private float maxDistance = 5.0f;

    private bool isInPlayerFaction;
    private Transform followTarget;
    public Rigidbody rb;

    public bool isLocked = false;
    public bool isSelected = false;
    public bool isDragging = false;

    //Harcoded, should do find
    private float playerSpeed = 7.5f;

    //TODO: Adjust values so they make sense
    private float max_velocity = 7.0f;
    private float max_force = 10.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
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
        //agent = GetComponent<NavMeshAgent>();
        //transformToFollow = GameObject.Find("Player").transform;
        followTarget = GameObject.Find("Player").transform;
    }

    public void FollowPlayer()
    {
        if(followTarget != null)
        {
            //Calculate desired velocity
            Vector3 desiredVelocity = (followTarget.position - transform.position).normalized;
            desiredVelocity *= max_velocity;

            //Steering force, without acceleration
            Vector3 steeringForce = (desiredVelocity - rb.velocity);
            steeringForce /= max_velocity;
            steeringForce *= max_force;

            //Steering force, with acceleration
            steeringForce = steeringForce / rb.mass;

            //Calculate rb's velocity
            Vector3 velocity = rb.velocity + steeringForce * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, max_velocity);
            rb.velocity = velocity;
            
        }
        
        /*
        if (followTarget != null)
        {
            //Distance between this NPC and the follow target
            float distance = Vector3.Distance(transform.position, followTarget.position);

            //If too far away from the target
            if (distance > maxDistance)
            {
                //Move in the direction of the target
                Vector3 direction = (followTarget.position - transform.position).normalized;

                //Smoothly change speed to match player's speed
                rb.velocity = Vector3.Lerp(
                    rb.velocity,
                    direction * playerSpeed,
                    Time.deltaTime * 6);
            }

            //If too close to target
            else if (distance < minDistance)
            {
                //Reversed direction is the direction away from the target, to move further away
                Vector3 reversedDirection = (transform.position - followTarget.position).normalized;

                //Smoothly change speed
                rb.velocity = Vector3.Lerp(
                    rb.velocity,
                    reversedDirection * playerSpeed,
                    Time.deltaTime * 6);
            }

            //If at a good distance from target, reduce speed to zero
            else
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 6);
            }
        }
        */
    }

    public void Lock()
    {
        isLocked = true;
        rb.isKinematic = true;
    }

    public void Unlock()
    {
        isLocked = false;
        rb.isKinematic = false;
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

    public void UpdateIsDragging_SC_Follower(bool _isDragging)
    {
        isDragging = _isDragging;
    }
}
