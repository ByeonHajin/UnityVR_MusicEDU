using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TalkTrigger : MonoBehaviour
{
    public TalkContainer talk;

    public void TriggerTalk() {
        FindObjectOfType<TalkManager>().StartTalk(talk);
    }

    [ContextMenu("To Json Data")]
    void SaveDataToJson()
    {
        string jsonData = JsonUtility.ToJson(talk,true);
        string path = Path.Combine(Application.dataPath,"talkData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadDataFromJson()
    {
        string path = Path.Combine(Application.dataPath,"talkData.json");
        string jsonData = File.ReadAllText(path);
        talk = JsonUtility.FromJson<TalkContainer>(jsonData);
    }
}

