using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_FaithSystem : MonoBehaviour
{
    private float totalFaith;
    private float totalFear;

    private const float limitFear = 2f;

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
        totalFaithValueText.GetComponent<TextMeshProUGUI>().text = totalFaith.ToString();

        if (totalFaith <= 0)
            SC_LevelManager.Instance.ResetLevel("Your followers lost their faith");
    }

    public void UpdateTotalFear(float increment)
    {
        if (increment < 0 && totalFear == 0)
            return;

        totalFear += increment;
        totalFearValueText.GetComponent<TextMeshProUGUI>().text = totalFear.ToString();

        if (totalFear > limitFear)
            SC_LevelManager.Instance.ResetLevel("Your followers became unable to handle their fear");
    }
}
