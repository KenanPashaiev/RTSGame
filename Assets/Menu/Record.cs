using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Record : MonoBehaviour
{
    public MatchData MatchData;

    public Text PlayerName;
    public Text Result;
    public Text BotsCount;
    public Text MatchTime;

    private void Start()
    {
        string hours = Mathf.Floor((MatchData.MatchTime % 21600) / 3600).ToString("00");
        string minutes = Mathf.Floor((MatchData.MatchTime % 3600) / 60).ToString("00");
        string seconds = (MatchData.MatchTime % 60).ToString("00");
        string time = hours + " : " + minutes + " : " + seconds;

        PlayerName.text = MatchData.PlayerName;
        Result.text = ResultToString(MatchData.Result);
        BotsCount.text = MatchData.BaseCount.ToString();
        MatchTime.text = time;
    }
    
    private string ResultToString(MatchResult result)
    {   
        var resultPairs = new Dictionary<MatchResult, string>();
        resultPairs.Add(MatchResult.victory, "Victory");
        resultPairs.Add(MatchResult.defeat, "Defeat");
        resultPairs.Add(MatchResult.leftMatch, "Left match");

        return resultPairs[result];
    }
}
