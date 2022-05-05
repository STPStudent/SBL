using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string sentences =  File.ReadAllText(@"Assets\Story.txt");
    public string[] sentencesList => sentences.Split('\n');
}