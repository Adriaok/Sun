using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EndingMonument : MonoBehaviour
{
    [SerializeField] private GameObject finishLevelUI;

    private void OnTriggerEnter(Collider other)
    {
        finishLevelUI.SetActive(true);
    }
}
