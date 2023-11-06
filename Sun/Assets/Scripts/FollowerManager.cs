using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public Dictionary<string, Follower> followers;
    private List<string> unavailableIDs;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void LockFollower(string id)
    {
        followers[id].isLocked = true;
    }

    void UnlockFollower(string id)
    {
        followers[id].isLocked = false;
    }

    void FollowPlayer()
    {
        foreach (KeyValuePair<string, Follower> follower in followers)
        {
            if(!follower.Value.isLocked)
            {
                follower.Value.FollowPlayer();
            }
        }
    }

    public void AddFollower()
    {
        //Generate random id

        //Check if id is not in unavailableIds

        //Create follower and add it to the dictionary with the id as key

        //Add new id to unavailableIds
    }
}
