using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chromachievement : ScriptableObject
{
    public Sprite Icon;
    [TextArea(3,10)] public string Text;
    public int StickerIndex;
}
