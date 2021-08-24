using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    
    public void TriggerDialog() {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void DisplayNextSentence() {
        Debug.Log("Event");
        FindObjectOfType<VideoManager>().DisplayNextSentence();
    }

    public void DisplayCurrentSentence(Dialog dialog, int idx) {
        FindObjectOfType<DialogManager>().DisplayCurrentSentence(dialog, idx);
    }

    [ContextMenu("To Json Data")]
    void SaveDataToJson()
    {
        string jsonData = JsonUtility.ToJson(dialog,true);
        string directoryPath = Path.Combine(Application.dataPath, dialog.name);
        string path = Path.Combine(Application.dataPath,dialog.name,"dialogData.json");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadDataFromJson()
    {
        string path = Path.Combine(Application.dataPath,dialog.name,"dialogData.json");
        string jsonData = File.ReadAllText(path);
        dialog = JsonUtility.FromJson<Dialog>(jsonData);
    }
}
