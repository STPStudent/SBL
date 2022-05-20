using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Dialogue
{
    [TextArea(3, 20)] public string sentences;
    public string[] sentencesList => sentences.Split('\n');
    
}