using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class NarrativeEventsPublisher : ScriptableObject
{
    public UnityEvent<Message[]> TriggerDialogue;
}
