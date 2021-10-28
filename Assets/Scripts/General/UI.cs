using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private float timeInMinutes;
    [SerializeField] private Text time;
    [SerializeField] private Text life;
    [SerializeField] private Text arrows;
    [SerializeField] private Text points;
    [SerializeField] private Text finalPoints;
    [SerializeField] private Text enemiesKilled;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    private bool gameOverTrigger =  false;
    private bool pauseTrigger = false;
    private bool winTrigger = false;
    public GameObject _gameOverScreen
    {
        get{ return this.gameOverScreen; }
        set{ this.gameOverScreen = value; }
    }
    public static UI instance;
    void Awake()
    {
        if(PlayerPrefs.GetInt("LastTime")>0)
        {
            timeInMinutes = PlayerPrefs.GetInt("LastTime");
        }
        timeInMinutes = timeInMinutes*60f;
        instance = this;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
    }
    void Update()
    {
        PauseGame();
        if(PlayerStats.playerStats != null)
        {
            life.text = PlayerStats.playerStats._life.ToString();
            arrows.text = PlayerStats.playerStats._arrows.ToString();
            points.text = PlayerStats.playerStats._points.ToString();
        }
        timeInMinutes -= Time.deltaTime;
        if(!pauseTrigger)
        {
            DisplayTime(timeInMinutes);
        }
        if(gameOverTrigger)
        {
            QuitResetButtons();
        }
        WinScreen();
        
    }
    /// <summary>
    /// A Function to show the game over Screen. 
    /// </summary>
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        gameOverTrigger = true; 
    }
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOverTrigger)
        {
            pauseTrigger = !pauseTrigger;
        }
        if(pauseTrigger && !winTrigger)
        {
           Time.timeScale = 0;
           QuitResetButtons();
           pauseScreen.SetActive(true);
        }
        else if(!pauseTrigger && !winTrigger && !gameOverTrigger)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
    private void WinScreen()
    {
        if(timeInMinutes<=0 && !gameOverTrigger)
        {
            timeInMinutes = 0f;
            winScreen.SetActive(true);
            Time.timeScale = 0;
            winTrigger = true;
            finalPoints.text = points.text;
            enemiesKilled.text = PlayerStats.playerStats._enemiesKilled.ToString();
            if(PlayerPrefs.GetInt("HighScore")<PlayerStats.playerStats._points)
            {
                PlayerPrefs.SetInt("HighScore",PlayerStats.playerStats._points);
            }            
            QuitResetButtons(); 
        }
    }
    private void QuitResetButtons()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
