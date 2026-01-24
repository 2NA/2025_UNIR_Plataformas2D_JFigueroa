using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;
    
    private bool gameOver = false;
    
    public static GameManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
    
    public void Restart()
    {
        gameOver = false;  // Reset estado
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    
    // public void MainMenu()
    // {
    //     Time.timeScale = 1f;
    //     SceneManager.LoadScene("SampleScene");
    // }
}
