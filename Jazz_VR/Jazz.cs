using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Jazz
{
    public AudioClip Drum;
    public Option[] Basses;
    public Option[] Pianos;
}

[System.Serializable]
public class Option {
    public string name;
    public AudioClip clip;
}
