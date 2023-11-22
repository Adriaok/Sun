using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_UI_FollowerActions : MonoBehaviour
{
    public List<GameObject> actionObjects = new List<GameObject>();

    //actionObjectsWithID[action_locked].SetActive(false); ...
    public Dictionary<string, GameObject> actionObjectsWithID = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitActionObjectsWithID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitActionObjectsWithID()
    {
        for(int i = 0; i < actionObjects.Count; i++)
        {
            actionObjectsWithID[actionObjects[i].name] = actionObjects[i];
        }
    }
}
