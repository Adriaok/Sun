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
    private Transform cameraTransform;
    private NavMeshAgent navMeshAgent;
    public Rigidbody rb;

    public bool isLocked = false;
    public bool isSelected = false;
    public bool isDragging = false;
    private bool isRotationLocked = false;
    private bool isThrowing = false;

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
    }

    private void FixedUpdate()
    {
        if (isThrowing)
        {
            BeThrown();
        }
    }

    public void Init(Transform _followTarget, Transform _cameraTransform)
    {
        //transformToFollow = GameObject.Find("Player").transform;
        //followTarget = GameObject.Find("Player").transform;
        followTarget = _followTarget;
        cameraTransform = _cameraTransform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        //transform.rotation = followTarget.rotation;
    }

    public void FollowPlayer()
    {
        navMeshAgent.destination = followTarget.position;

        if (!isRotationLocked)
        {
            transform.Rotate(new Vector3(
                0.0f, 
                cameraTransform.rotation.y - transform.rotation.y, 
                0.0f)
            );
        }
        else
        {
            navMeshAgent.angularSpeed = 0;
        }

        Debug.Log("Follow target");
            
    }

    private void BeThrown()
    {
        //With the left click...
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            //Debug.Log(mousePos);
            mousePos.z = 0.1f;
            Camera camera = GameObject.FindAnyObjectByType<Camera>();
            Vector3 screenPos = camera.ScreenToWorldPoint(mousePos);
            Debug.Log(screenPos);
            rb.AddRelativeForce(new Vector3(
                screenPos.x * 100f,
                10000f,
                screenPos.z * 100f
                ));

            //rb.AddForce(screenPos * 100f);
            //GetComponent<Rigidbody>().velocity = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            //GetComponent<Rigidbody>().velocity = (screenPos - camera.transform.position) * 0.5f;
             
            /*
            Camera camera = GameObject.FindAnyObjectByType<Camera>();
            Ray r = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = r.GetPoint(1) - r.GetPoint(0);
            transform.rotation = Quaternion.LookRotation(dir);
            rb.velocity = transform.forward * 20;
            */

            isThrowing = false;
            isSelected = false;
            BroadcastMessage("UpdateIsSelected_SC_Target", false);
        }
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

    public void LockRotation_SC_Follower()
    {
        isRotationLocked = !isRotationLocked;
        Debug.Log("Rotation locked = " + isRotationLocked);
    }

    public void ToggleLight_SC_Follower()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        BroadcastMessage("ToggleLight", GetComponent<Light>().enabled);
    }

    public void UpdateIsThrowing_SC_Follower()
    {
        isThrowing = true;
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
