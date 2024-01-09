using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class UIEventsPublisher : ScriptableObject
{
    public UnityEvent FadeToBlackFinished;
    public UnityEvent FirstFadeFinished;
    public UnityEvent MessadeFadeOutFinished;
}
