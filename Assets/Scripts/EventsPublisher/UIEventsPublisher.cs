using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "EventsPublisher/UIEventsPublisher")]
public class UIEventsPublisher : ScriptableObject
{
    public UnityEvent FadeToBlackFinished;
    public UnityEvent FirstFadeFinished;
    public UnityEvent MessageFadeOutFinished;
    public UnityEvent<bool, bool> NotebookFlipped;
}
