using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chromachievement : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    [TextArea(3,10)] public string DescriptionText;
    public int ID;
}
