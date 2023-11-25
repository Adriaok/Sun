using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_UI_MessageManager : MonoBehaviour
{
    private static SC_UI_MessageManager _instance;

    [SerializeField]
    private GameObject messagePrefab;

    public static SC_UI_MessageManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage(string text)
    {
        GameObject newMessage = Instantiate(messagePrefab, transform);
        newMessage.GetComponent<TextMeshProUGUI>().text = text;
    }
}
