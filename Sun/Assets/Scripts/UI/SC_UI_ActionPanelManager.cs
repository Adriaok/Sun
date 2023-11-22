using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_ActionPanelManager : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();

    //panelsWithID[followerID].SetActive(true); ...
    public Dictionary<string, GameObject> panelsWithID = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPanelsWithID()
    {

    }
}
