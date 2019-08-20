using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPrefab;
    public GameObject PlayMenuPrefab;
    public GameObject RecordsMenuPrefab;

    public void OnPlayButtonClick()
    {
        PlayMenuPrefab.SetActive(true);
        MainMenuPrefab.SetActive(false);
    }

    public void OnRecordsButtonClick()
    {
        RecordsMenuPrefab.SetActive(true);
        MainMenuPrefab.SetActive(false);
    }
}
