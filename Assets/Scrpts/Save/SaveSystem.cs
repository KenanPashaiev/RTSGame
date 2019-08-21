using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveRecord(string playerName, int baseCount, float matchTime, MatchResult result)
    {
        var matchData = new MatchData(playerName, baseCount, matchTime, result);
        var recordList = LoadRecords();

        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.sav";
        var fileStream = new FileStream(path, FileMode.OpenOrCreate);

        if (recordList == null)
        {
            recordList = new List<MatchData>();
        }
        recordList.Add(matchData);

        formatter.Serialize(fileStream, recordList);
        fileStream.Close();
    }

    public static List<MatchData> LoadRecords()
    {
        string path = Application.persistentDataPath + "/save.sav";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var fileStream = new FileStream(path, FileMode.Open);

            if (fileStream.Length == 0)
            {
                fileStream.Close();
                return null;
            }
            var recordList = formatter.Deserialize(fileStream) as List<MatchData>;
            fileStream.Close();

            return recordList;
        }

        return null;
    }
}
