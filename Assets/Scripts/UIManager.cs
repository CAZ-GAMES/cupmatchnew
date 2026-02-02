using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        GameManager.Instance.moves++;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
    }

    void ShowScore()
    {
        print(GameManager.Instance.moves);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CheckButton()
    {
        GameManager.Instance.UpdateGameState(GameState.Check);
    }
}
