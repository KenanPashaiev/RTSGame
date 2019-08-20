using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MatchData
{
    public string PlayerName;
    public int BaseCount;
    public float MatchTime;
    public string Result;

    public MatchData(string playerName, int baseCount, float matchTime, string result)
    {
        PlayerName = playerName;
        BaseCount = baseCount;
        MatchTime = matchTime;
        Result = result;
    }
}
