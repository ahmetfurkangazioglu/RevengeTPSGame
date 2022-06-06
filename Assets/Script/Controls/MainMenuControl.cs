using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject SurePanel;
   public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit(string Result)
    {
        switch (Result)
        {
            case "Sure":
                SurePanel.SetActive(true);
                break;
            case "No":
                SurePanel.SetActive(false);
                break;
            case "Yes":
                Application.Quit();
                break;
        }
    }
}
