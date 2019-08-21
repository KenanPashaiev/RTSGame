using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MainMenu MainMenuPrefab;
    public PlayMenu PlayMenuPrefab;
    public RecordMenu RecordsMenuPrefab;

    public void OnPlayButtonClick()
    {
        PlayMenuPrefab.ShowMenu();
        MainMenuPrefab.HideMenu();
    }

    public void OnRecordsButtonClick()
    {
        RecordsMenuPrefab.ShowMenu();
        MainMenuPrefab.HideMenu();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }
}
