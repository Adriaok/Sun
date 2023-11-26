using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlagMonument : MonoBehaviour
{
    private bool isUsed;
    private int currentFollowers;

    [SerializeField] private Transform player;
    [SerializeField] private FollowerManager followerManager;
    [SerializeField] private int minFollowerRequirement;
    private ParticleSystem particles;

    [SerializeField] private List<SC_Target> followerList;

    [SerializeField] private CameraController camera;
    private float monumentOffset = 19f;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        currentFollowers = 0;
        foreach (KeyValuePair<string, GameObject> follower in followerManager.followers)
        {
            if (follower.Value.GetComponent<SC_Follower>().isInPlayerFaction)
                currentFollowers++;
        }

        if (Vector3.Distance(player.position, transform.position) <= 10f &&
        currentFollowers >= minFollowerRequirement &&
        Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ResponseToMonument());

            particles.startSpeed = 7.5f;
            particles.startSize = 1f;
            particles.emissionRate = 15f;

            foreach (SC_Target follower in followerList)
            {
                follower.CheckIfRecruiting();
            }
        }

    }
    float t = 0.0f;
    public float minimum = -1.0F;
    public float maximum = 1.0F;

    IEnumerator ResponseToMonument()
    {
        float temp = camera._transposer.m_FollowOffset.y;
        camera._transposer.m_FollowOffset.y = monumentOffset;

        yield return new WaitForSeconds(5);

        camera._transposer.m_FollowOffset.y = temp;
        particles.enableEmission = false;
    }
}
