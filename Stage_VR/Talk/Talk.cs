using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Talk
{
    //public string name;

    //[TextArea(3, 10)]
    //public string[] sentences;
    //public TalkContent[] sentences;//TextArea => 최소, 최대 설정
}

[System.Serializable]
public class TalkContainer
{
    public string name;

    public List<TalkData> talkDatas;
}

[System.Serializable]
public class TalkData
{
    public string sentence;
    public int isEvent;
}
 