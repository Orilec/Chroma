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
    public UnityEvent<Sprite> CardCollected;
    public UnityEvent<bool> EnterValidateLevel;
    public UnityEvent EndOfVSLevel;
    public UnityEvent<int> AddingPages;
}
