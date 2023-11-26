using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_LevelManager : MonoBehaviour
{
    private static SC_LevelManager _instance;

    public static SC_LevelManager Instance { get { return _instance; } }


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

    public void ResetLevel()
    {
        Debug.Log("Reset scene");
        SceneManager.LoadScene("Level1");
    }
}
