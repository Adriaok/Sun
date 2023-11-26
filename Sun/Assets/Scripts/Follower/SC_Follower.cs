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
    public Transform cameraTransform;
    private NavMeshAgent navMeshAgent;
    public Rigidbody rb;

    public bool isLocked = false;
    public bool isSelected = false;
    public bool isDragging = false;
    private bool isRotationLocked = false;
    private bool isThrowing = false;

    public string ID;
    public float lightRange = 20.0f;

    private float fearRadius = 40.0f;
    private float timeSinceFearUpdateStart = 0.0f;
    private float maxUpdateTime = 20.0f;
    private bool isAfraid = false;
    private int fear = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrowing)
        {
            BeThrown();
        }

        if (isInPlayerFaction)
        {
            CheckIsAfraid();
            CheckFearUpdate();
        }
    }

    private void FixedUpdate()
    {
        
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
            Debug.Log("Throw");         
            Camera camera = GameObject.FindAnyObjectByType<Camera>();
            Ray r = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = r.GetPoint(1) - r.GetPoint(0);
            transform.rotation = Quaternion.LookRotation(dir);
            rb.velocity = transform.forward * 20;
            

            isThrowing = false;
            isSelected = false;
            BroadcastMessage("UpdateIsSelected_SC_Target", false);
        }
    }

    private void CheckIsAfraid()
    {
        float distanceToPlayer = Vector3.Distance(followTarget.position, transform.position);
        if (isLocked && distanceToPlayer > fearRadius)
        {
            isAfraid = true;
        }
        else
        {
            isAfraid = false;
        }
    }

    private void CheckFearUpdate()
    {
        if(isAfraid && timeSinceFearUpdateStart < maxUpdateTime)
        {
            fear += 1;
            if (fear % 500 == 0)
            {
                SC_FaithSystem.Instance.UpdateTotalFear(1f);
            }

            timeSinceFearUpdateStart += Time.deltaTime;
        }
        else if(isAfraid && timeSinceFearUpdateStart > maxUpdateTime)
        {
            Debug.Log("out of time");
            BeSacrificed();
        }
    }

    public void Lock()
    {
        isLocked = true;
        navMeshAgent.isStopped = true;
        SC_UI_MessageManager.Instance.ShowMessage("Lock follower");
        //rb.isKinematic = true;
    }

    public void Unlock()
    {
        isLocked = false;
        navMeshAgent.isStopped = false;
        SC_UI_MessageManager.Instance.ShowMessage("Unlock follower");
        //rb.isKinematic = false;
    }

    public void Die()
    {
        Debug.Log("Die");
        this.gameObject.SetActive(false);
    }

    private void BeSacrificed()
    {
        Die();
        SC_FaithSystem.Instance.UpdateTotalFear(10f);
        SC_UI_MessageManager.Instance.ShowMessage("A follower has been sacrificed");
    }

    public void ChangeIsAfraid()
    {
        isAfraid = !isAfraid;
    }

    public void SetNavMeshAgentIsStopped(bool _value)
    {
        navMeshAgent.isStopped = _value;
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
        
        if(isRotationLocked)
            SC_UI_MessageManager.Instance.ShowMessage("Lock rotation");
        else
            SC_UI_MessageManager.Instance.ShowMessage("Unlock rotation");
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
