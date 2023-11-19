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

    public bool isInPlayerFaction = false;
    private Transform followTarget;
    private NavMeshAgent navMeshAgent;
    public Rigidbody rb;

    public bool isLocked = false;
    public bool isSelected = false;
    public bool isDragging = false;

    public string ID;
    public float lightRange = 20.0f;

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
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!isLocked)  //TODO: Consider locked flock too
        {
            followTarget = GameObject.Find("Player").transform;
            transform.rotation = followTarget.rotation;
        }
        */
    }

    public void Init(Transform _followTarget)
    {
        //transformToFollow = GameObject.Find("Player").transform;
        //followTarget = GameObject.Find("Player").transform;
        followTarget = _followTarget;
        navMeshAgent = GetComponent<NavMeshAgent>();
        transform.rotation = followTarget.rotation;
    }

    public void FollowPlayer()
    {
        //if(followTarget != null)
        //{
            /*
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
            */
            navMeshAgent.destination = followTarget.position;
            transform.rotation = followTarget.rotation;
            Debug.Log("Follow target");
            
        //}
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

    public void UpdateIsInPlayerFaction_SC_Follower(bool _value)
    {
        isInPlayerFaction = _value;
    }

    public void Rotate_SC_Follower(float _rotation)
    {
        Quaternion target = Quaternion.Euler(0, _rotation, 0);
        transform.Rotate(new Vector3(0.0f, _rotation, 0.0f));
        Debug.Log("Rotate: " + transform.rotation.y);
    }

    public void ToggleLight_SC_Follower()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        BroadcastMessage("ToggleLight", GetComponent<Light>().enabled);
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
