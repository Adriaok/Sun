using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_FaithSystem : MonoBehaviour
{
    private float totalFaith;
    private float totalFear;

    public GameObject totalFaithValueText;
    public GameObject totalFearValueText;


    private static SC_FaithSystem _instance;

    public static SC_FaithSystem Instance { get { return _instance; } }


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

    public void UpdateTotalFaith(float increment)
    {
        totalFaith += increment;
        Debug.Log(totalFaithValueText.GetComponent<TextMeshProUGUI>().text);
        totalFaithValueText.GetComponent<TextMeshProUGUI>().text = totalFaith.ToString();
        Debug.Log("Update total faith");
    }

    public void UpdateTotalFear(float increment)
    {
        if (increment < 0 && totalFear == 0)
            return;

        totalFear += increment;
        totalFearValueText.GetComponent<TextMeshProUGUI>().text = totalFear.ToString();
    }
}
