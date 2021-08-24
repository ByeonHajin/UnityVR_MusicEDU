using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Dialog
{
    public string name;
    public List<DialogData> dialogs;

}

[System.Serializable]
public class DialogData
{
    public string sentence;
    public int isEvent;
}
