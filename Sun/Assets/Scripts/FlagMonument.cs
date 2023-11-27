using System.Collections;
using System.Collections.Generic;
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
    private float monumentOffset = 15f;
    private bool hasShownMessage = false;

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

        if(Vector3.Distance(player.position, transform.position) <= 10f && !hasShownMessage)
        {
            if (SC_UI_MessageManager.Instance.canShowMessages)
            {
                SC_UI_MessageManager.Instance.ShowMessage("Press 'E' to interact with the monument");
                hasShownMessage = true;
            }
        }

        if (Vector3.Distance(player.position, transform.position) <= 10f &&
        currentFollowers >= minFollowerRequirement &&
        Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ResponseToMonument());

            particles.startSpeed = 7.5f;
            particles.startSize = 1f;
            particles.emissionRate = 15f;

            foreach (SC_Target follower in followerList)
            {
                follower.CheckIfRecruiting();
            }

            SC_FaithSystem.Instance.UpdateTotalFaith(10f + 10f * currentFollowers);
        }

    }

    IEnumerator ResponseToMonument()
    {
        float tempY = camera._transposer.m_FollowOffset.y;
        float tempZ = camera._transposer.m_FollowOffset.z;
        camera._transposer.m_FollowOffset.y += monumentOffset;
        camera._transposer.m_FollowOffset.z = -0.5f;

        Debug.Log(monumentOffset);
     
        yield return new WaitForSeconds(2);

        camera._transposer.m_FollowOffset.y = tempY;
        camera._transposer.m_FollowOffset.z = tempZ;

        Debug.Log(monumentOffset);

        particles.enableEmission = false;
        this.enabled = false;
    }
}
