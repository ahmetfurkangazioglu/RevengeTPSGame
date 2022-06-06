using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject GameOverPanel;

    void Start()
    {
        
    }
    public void Lose()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        WinPanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
}
