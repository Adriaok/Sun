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

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= 10f &&
            followerManager.followers.Count >= minFollowerRequirement && 
            Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("in");

            particles.startSpeed = 7.5f;
            particles.startSize = 1f;
            particles.emissionRate = 15f;
        }
    }
}
