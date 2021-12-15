using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField usernameInput;
    public TextMeshProUGUI highScoreText;

    // at the start of the game, get latest high score
    public void Start()
    {
        GetHighScore();
    }

    // get the user name input and set it in a variable of persistence manager
    public void GetUserName()
    {
        PersistenceManager.Instance.nameString = usernameInput.text;
    }


    public void GetHighScore()
    {
        highScoreText.text = "High Score : " + PersistenceManager.Instance.highScoreUser + " : " + PersistenceManager.Instance.highScore;

    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        GetUserName();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
