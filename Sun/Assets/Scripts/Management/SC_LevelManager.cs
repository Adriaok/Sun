using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

struct Solution
{
    List<Dictionary<int, string>> followers;
}

struct Puzzle
{
    const string WHISP_TYPE = "whisp";
    const string LANTERN_TYPE = "lantern";
    const string FLASHLIGHT_TYPE = "flashlight";

    Dictionary<int, Solution> solutions;
    bool completed;
}


public class SC_LevelManager : MonoBehaviour
{
    private List<Puzzle> puzzles = new List<Puzzle>();


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
