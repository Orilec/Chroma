using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class PlayerEventsPublisher : ScriptableObject
{
    public UnityEvent Respawn;
    public UnityEvent LeavingGround;
    public UnityEvent EnteringGround;
    public UnityEvent<int> CardCollected;
    public UnityEvent<bool> PauseGame;
    public UnityEvent<bool> EnterValidateLevel;
}
