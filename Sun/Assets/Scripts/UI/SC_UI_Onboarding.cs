using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Onboarding : MonoBehaviour
{
    private int currentText;

    [SerializeField]
    private List<GameObject> texts = new List<GameObject>();

    [SerializeField]
    private GameObject onboardingUI;

    // Start is called before the first frame update
    void Start()
    {
        currentText = 0;
        texts[currentText].SetActive(true);
    }

    public void ChangeToNextText()
    {
        texts[currentText].SetActive(false);
        currentText++;

        if (currentText < texts.Count)
            texts[currentText].SetActive(true);
        else
        {
            onboardingUI.SetActive(false);
            SC_UI_MessageManager.Instance.canShowMessages = true;
        }
    }
}
