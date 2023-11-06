using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public GameObject followerPrefab;
    
    public Dictionary<string, GameObject> followers = new Dictionary<string, GameObject>();
    private List<string> unavailableIDs = new List<string>();

    //Test
    float elapsedTime = 0.0f;
    float secondsBetweenSpawn = 2.0f;
    int maxFollowers = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        CheckActionsOnFollowers();
        
        //Test
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn && followers.Count < maxFollowers)
        {
            elapsedTime = 0.0f;
            AddFollower();
        }
    }

    void LockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().isLocked = true;
        followers[id].GetComponent<SC_Follower>().UpdateIsSelectedAndBroadcast(false);
    }

    void UnlockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().isLocked = false;
        followers[id].GetComponent<SC_Follower>().UpdateIsSelectedAndBroadcast(false);
    }

    void CheckActionsOnFollowers()
    {
        CheckIsLockedOnFollowers();
    }

    void CheckIsLockedOnFollowers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (KeyValuePair<string, GameObject> follower in followers)
            {

                if (follower.Value.GetComponent<SC_Follower>().isSelected && follower.Value.GetComponent<SC_Follower>().isLocked)
                {
                    UnlockFollower(follower.Key);
                    Debug.Log("Unlock " + follower.Key);
                }
                else if (follower.Value.GetComponent<SC_Follower>().isSelected && !follower.Value.GetComponent<SC_Follower>().isLocked)
                {
                    LockFollower(follower.Key);
                    Debug.Log("Lock " + follower.Key);
                }
            }

        }
    }

    void FollowPlayer()
    {
        foreach (KeyValuePair<string, GameObject> follower in followers)
        {
            if(!follower.Value.GetComponent<SC_Follower>().isLocked)
            {
                follower.Value.GetComponent<SC_Follower>().FollowPlayer();
                Debug.Log(follower.Key + " is following player");
            }
        }
    }

    public void AddFollower()
    {   
        //Generate random id & check if id is not in unavailableIds
        bool validID = false;
        string newID;

        do
        {
            newID = GenerateID(10);

            if (unavailableIDs.Count == 0)
                validID = true;

            foreach(string id in unavailableIDs)
            {
                if (newID == id)
                {
                    validID = false;
                    break;
                }
                else
                    validID = true;
            }

        } while (!validID);

        //Create follower and add it to the dictionary with the id as key
        GameObject newFollower = Instantiate(followerPrefab, new Vector3(Random.Range(-10, 10), 1, 0), Quaternion.identity);
        newFollower.GetComponent<SC_Follower>().Init();
        followers[newID] = newFollower;

        //Add new id to unavailableIds
        unavailableIDs.Add(newID);
    }

    string GenerateID(int charAmount)
    {
        const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
        string generatedID = "";

        for (int i = 0; i < charAmount; i++)
        {
            generatedID += glyphs[Random.Range(0, glyphs.Length)];
        }

        return generatedID;
    }
}
