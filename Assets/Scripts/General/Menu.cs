using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private InputField time;
    [SerializeField] private InputField initialCount;
    [SerializeField] private Text hiScore;
    // Start is called before the first frame update
    void Start()
    {
        hiScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        time.text = PlayerPrefs.GetInt("LastTime").ToString();
        initialCount.text = PlayerPrefs.GetInt("InitialCount").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        if(int.Parse(time.text) == 0)
        {
            time.text = 3.ToString();
        }
        if(int.Parse(initialCount.text) == 0)
        {
            initialCount.text = 3.ToString();
        }
        PlayerPrefs.SetInt("LastTime", int.Parse(time.text)); 
        PlayerPrefs.SetInt("InitialCount", int.Parse(initialCount.text));     
    }
}
