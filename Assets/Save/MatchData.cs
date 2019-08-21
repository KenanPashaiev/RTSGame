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
    public MatchResult Result;

    public MatchData(string playerName, int baseCount, float matchTime, MatchResult result)
    {
        PlayerName = playerName;
        BaseCount = baseCount;
        MatchTime = matchTime;
        Result = result;
    }
}
