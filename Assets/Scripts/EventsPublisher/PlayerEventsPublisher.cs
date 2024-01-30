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
    public UnityEvent<bool> Jumping;
    public UnityEvent<bool> Sliding;
    public UnityEvent<bool> AirSliding;
    public UnityEvent<bool> SlideJumping;
    public UnityEvent<bool> Dying;
    public UnityEvent<bool> GroundedState;
    public UnityEvent<bool> LandingToLower;
    public UnityEvent<float> LocomotionSpeed;
    public UnityEvent<Sprite> CardCollected;
    public UnityEvent<bool> PauseGame;
    public UnityEvent<bool> EnterValidateLevel;
    public UnityEvent<int> AddingPages;

    public void DebugCollectible()
    {
        CardCollected.Invoke(null);
    }

    public void DebugPages()
    {
        AddingPages.Invoke(2);
    }
}
