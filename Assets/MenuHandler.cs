using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject[] mainMenuOptions;
    [SerializeField] GameObject tutorialObject;
    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTutorial()
    {
        foreach(var screen in mainMenuOptions)
        {
            screen.SetActive(false);
        }
        tutorialObject.SetActive(true);
    }

    public void HideTutorial()
    {
        foreach (var screen in mainMenuOptions)
        {
            screen.SetActive(true);
        }
        tutorialObject.SetActive(false);
    }
}
