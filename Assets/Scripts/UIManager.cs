using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI moves;


    void Awake()
    {
        GameManager.OnGameStateChange += ShowScore; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void ODestroy()
    {
        GameManager.OnGameStateChange -= ShowScore;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        GameManager.Instance.coolOff = false;
        GameManager.Instance.moves = 0;
        GameManager.Instance.correctMatches = 0;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
    }

    void ShowScore(GameState state)
    {
        // update score after swap switches back to trying
        if (state == GameState.Trying)
        {
            moves.text = "Moves: " + GameManager.Instance.moves;
            moves.text += "\nCorrect Matches: " + GameManager.Instance.correctMatches;
        }
        else if (state == GameState.Win)
        {
            moves.text = "You Won in " + GameManager.Instance.moves + " moves!";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CheckButton()
    {
        if (GameManager.Instance.coolOff == false)
            GameManager.Instance.UpdateGameState(GameState.Check);
    }
}
