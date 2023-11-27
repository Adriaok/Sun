using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public GameObject followerPrefab;
    public GameObject whispPrefab;
    public GameObject lanternPrefab;
    public GameObject flashlightPrefab;
    public Transform playerTransform;
    public Transform cameraTransform;

    [SerializeField]
    public List<GameObject> rawFollowers = new List<GameObject>();

    public Dictionary<string, GameObject> followers = new Dictionary<string, GameObject>();
    private List<string> unavailableIDs = new List<string>();

    //If true, the flock won't move. If false, it will follow the player
    private bool lockedFlock = false;


    //Test
    float elapsedTime = 0.0f;
    float secondsBetweenSpawn = 2.0f;
    int maxFollowers = 3;

    // Start is called before the first frame update
    void Start()
    {
        IdentifyFollowers();
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockedFlock)
            FollowPlayer();

        CheckActionsOnFollowers();
        RemoveDeadFollowers();
        //Test
        /*
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn && followers.Count < maxFollowers)
        {
            elapsedTime = 0.0f;
            AddFollower();
        }
        */
        
    }

    private void RemoveDeadFollowers()
    {
        foreach (KeyValuePair<string, GameObject> follower in followers)
        {
            if (follower.Value == null)
            {
                followers.Remove(follower.Key);
            }
        }

        Debug.Log("Follower count: " + followers.Count);

        if (followers.Count == 0)
            SC_LevelManager.Instance.ResetLevel();
    }

    void LockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().Lock();
        followers[id].GetComponent<SC_Follower>().UpdateIsSelectedAndBroadcast(false);
    }

    void UnlockFollower(string id)
    {
        followers[id].GetComponent<SC_Follower>().Unlock();
        followers[id].GetComponent<SC_Follower>().UpdateIsSelectedAndBroadcast(false);
    }

    void CheckActionsOnFollowers()
    {
        CheckIsLockedOnFollowers();
        CheckIsLockedOnFlock();
        CheckRecallAllFollowers();
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

    void SetFollowersRbIsKinematic(bool _isKinematic)
    {
        foreach (KeyValuePair<string, GameObject> follower in followers)
        {
            follower.Value.GetComponent<SC_Follower>().rb.isKinematic = _isKinematic;
        }
    }

    void CheckIsLockedOnFlock()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (lockedFlock)
            {
                lockedFlock = false;
                //SetFollowersRbIsKinematic(false);
                Debug.Log("Unlock entire flock");

                foreach (KeyValuePair<string, GameObject> follower in followers)
                {
                    if(!follower.Value.GetComponent<SC_Follower>().isLocked)
                        follower.Value.GetComponent<SC_Follower>().SetNavMeshAgentIsStopped(lockedFlock);
                }

                SC_UI_MessageManager.Instance.ShowMessage("Lock flock");
            }
            else
            {
                lockedFlock = true;
                //SetFollowersRbIsKinematic(true);
                Debug.Log("Lock entire flock");

                foreach (KeyValuePair<string, GameObject> follower in followers)
                {
                    follower.Value.GetComponent<SC_Follower>().SetNavMeshAgentIsStopped(lockedFlock);
                }

                SC_UI_MessageManager.Instance.ShowMessage("Unlock flock");
            }
        }
    }

    void CheckRecallAllFollowers()
    {
        if(/*Input.GetKeyDown(KeyCode.Space) &&*/ Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (KeyValuePair<string, GameObject> follower in followers)
            {
                if (follower.Value.GetComponent<SC_Follower>().isLocked)
                    UnlockFollower(follower.Key);
            }

            Debug.Log("Recall all followers");
        }
    }

    void FollowPlayer()
    {
        List<string> followersToRemove = new List<string>();
        foreach (KeyValuePair<string, GameObject> follower in followers)
        {
            if(!follower.Value.GetComponent<SC_Follower>().isLocked && follower.Value.GetComponent<SC_Follower>().isInPlayerFaction && follower.Value.GetComponent<SC_Follower>().gameObject.active)
            {
                follower.Value.GetComponent<SC_Follower>().FollowPlayer();
            }
            else if(!follower.Value.GetComponent<SC_Follower>().gameObject.active)
            {
                followersToRemove.Add(follower.Key);
            }
        }

        foreach(string key in followersToRemove)
        {
            followers.Remove(key);
            Debug.Log("remove");
        }
    }

    private void IdentifyFollowers()
    {
        for(int i = 0; i < rawFollowers.Count; i++)
        {
            //Generate random id & check if id is not in unavailableIds
            bool validID = false;
            string newID;

            do
            {
                newID = GenerateID(10);

                if (unavailableIDs.Count == 0)
                    validID = true;

                foreach (string id in unavailableIDs)
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

            //Add follower to the dictionary with the new id as key
            followers[newID] = rawFollowers[i];
            followers[newID].GetComponent<SC_Follower>().Init(playerTransform, cameraTransform);
            followers[newID].GetComponent<SC_Follower>().ID = newID;
            followers[newID].layer = 7;

            //Add new id to unavailableIds
            unavailableIDs.Add(newID);
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
        
        followers[newID] = InstantiateWhisp(newID);

        //Add new id to unavailableIds
        unavailableIDs.Add(newID);
    }

    private GameObject InstantiateWhisp(string newID)
    {
        GameObject newFollower = Instantiate(whispPrefab, new Vector3(37.6669273f, 10, 66.4128876f), Quaternion.identity);
        newFollower.GetComponent<SC_Follower>().Init(playerTransform, cameraTransform);
        newFollower.GetComponent<SC_Follower>().ID = newID;
        newFollower.layer = 7;

        return newFollower;

    }

    private GameObject InstantiateLantern(string newID)
    {
        GameObject newFollower = Instantiate(lanternPrefab, new Vector3(37.6669273f, 10, 66.4128876f), Quaternion.identity);
        newFollower.GetComponent<SC_Follower>().Init(playerTransform, cameraTransform);
        newFollower.GetComponent<SC_Follower>().ID = newID;
        newFollower.layer = 7;

        return newFollower;
    }

    private GameObject InstantiateFlashlight(string newID)
    {
        GameObject newFollower = Instantiate(flashlightPrefab, new Vector3(37.6669273f, 10, 66.4128876f), Quaternion.identity);
        newFollower.GetComponent<SC_Follower>().Init(playerTransform, cameraTransform);
        newFollower.GetComponent<SC_Follower>().ID = newID;
        newFollower.layer = 7;

        return newFollower;
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
