using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class NarrativeEventsPublisher : ScriptableObject
{
    public UnityEvent<Message[]> TriggerDialogue;
    public UnityEvent<CinemachineVirtualCamera, bool> TriggerCamera;
}
