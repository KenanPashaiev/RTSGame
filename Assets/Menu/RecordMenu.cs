using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordMenu : MonoBehaviour
{
    public MainMenu MainMenuPrefab;
    public RecordMenu RecordsMenuPrefab;

    public Record RecordPrefab;

    public RectTransform ContentBox;

    public void OnEscapeButtonClick()
    {
        RecordsMenuPrefab.HideMenu();
        MainMenuPrefab.ShowMenu();
    }

    private void AddRecords()
    {
        var recordList = SaveSystem.LoadRecords() as List<MatchData>;
        if (recordList == null)
        {
            return;
        }

        var contentBoxHeight = recordList.Count * 210;
        ContentBox.sizeDelta = new Vector2(ContentBox.sizeDelta.x, contentBoxHeight);

        for (int i = 0; i < recordList.Count; i++)
        {
            var record = recordList[i];
            Record recordBoxClone;
            recordBoxClone = Instantiate(RecordPrefab, new Vector3(), new Quaternion());
            recordBoxClone.MatchData = record;//Add record to record box
            recordBoxClone.transform.SetParent(ContentBox.transform);//Set content box as parent of record box

            var recordBoxHeight = recordBoxClone.GetComponent<RectTransform>().sizeDelta.y;

            recordBoxClone.GetComponent<RectTransform>().localPosition = new Vector2(0, i * 210 * -1);
        }
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        AddRecords();
    }
}