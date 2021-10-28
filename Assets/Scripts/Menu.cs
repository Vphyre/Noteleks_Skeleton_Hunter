using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private InputField time;
    [SerializeField] private Text hiScore;
    // Start is called before the first frame update
    void Start()
    {
        hiScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        time.text = PlayerPrefs.GetInt("LastTime").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadingScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        if(int.Parse(time.text)==0)
        {
            time.text = 3.ToString();
        }
        print(time.text);
        PlayerPrefs.SetInt("LastTime", int.Parse(time.text)); 
        LoadingScene();
    }
}
