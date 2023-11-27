using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Solution
{
    private Dictionary<string, int> necessaryFollowers;

    public void InitSolution(Dictionary<string, int> _necessaryFollowers)
    {
        necessaryFollowers = new Dictionary<string, int>();
        necessaryFollowers = _necessaryFollowers;
    }
}

public class Puzzle
{
    private Dictionary<int, Solution> solutions;
    public bool completed;

    public void InitPuzzle(List<Solution> _solutionList)
    {
        solutions = new Dictionary<int, Solution>();
        for(int i = 0; i < _solutionList.Count; i++)
        {
            solutions[i] = _solutionList[i];
        }

        completed = false;
    }
}


public class SC_LevelManager : MonoBehaviour
{
    private const string WHISP_TYPE = "whisp";
    private const string LANTERN_TYPE = "lantern";
    private const string FLASHLIGHT_TYPE = "flashlight";

    [SerializeField] private GameObject levelResetUI;
    [SerializeField] private GameObject levelResetText;

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

    private void Start()
    {
        InitPuzzles();
    }

    private void InitPuzzles()
    {
        //First puzzle -> make function?
        Solution solution = new Solution();
        Dictionary<string, int> necessaryFollowers = new Dictionary<string, int>();
        necessaryFollowers[WHISP_TYPE] = 1;
        necessaryFollowers[LANTERN_TYPE] = 1;
        necessaryFollowers[FLASHLIGHT_TYPE] = 1;
        solution.InitSolution(necessaryFollowers);

        Puzzle puzzle = new Puzzle();
        List<Solution> solutionsList = new List<Solution>();
        solutionsList.Add(solution);
        puzzle.InitPuzzle(solutionsList);
    }

    public void ResetLevel(string messageText)
    {
        levelResetUI.SetActive(true);
        levelResetText.GetComponent<TextMeshProUGUI>().text = messageText;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
