using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagMonument : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private FollowerManager followerManager;
    [SerializeField] private int minFollowerRequirement;
    private ParticleSystem particles;

    [SerializeField] private List<SC_Target> followerList;

    private int currentFollowers;

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
            Debug.Log("in");

            particles.startSpeed = 7.5f;
            particles.startSize = 1f;
            particles.emissionRate = 15f;

            foreach (SC_Target follower in followerList)
            {
                follower.CheckIfRecruiting();
            }
        }
    }
}
