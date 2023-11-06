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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Test
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0.0f;
            AddFollower();
        }

        FollowPlayer();
    }

    void LockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().isLocked = true;
    }

    void UnlockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().isLocked = false;
    }

    void FollowPlayer()
    {
        foreach (KeyValuePair<string, GameObject> follower in followers)
        {
            if(!follower.Value.GetComponent<SC_Follower>().isLocked)
            {
                follower.Value.GetComponent<SC_Follower>().FollowPlayer();
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
        GameObject newFollower = Instantiate(followerPrefab, new Vector3(3, 1, 0), Quaternion.identity);
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
