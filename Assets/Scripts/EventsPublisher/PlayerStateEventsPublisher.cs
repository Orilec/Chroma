using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class PlayerStateEventsPublisher : ScriptableObject
{
    public UnityEvent Respawn;
    public UnityEvent<int> CardCollected;
    public UnityEvent<bool> PauseGame;
}
