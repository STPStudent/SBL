using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Dialogue
{
    private string sentences =  File.ReadAllText(@"Assets\Story.txt");
    public string[] sentencesList => sentences.Split('\n');
}