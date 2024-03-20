using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "EventsPublisher/PlayerEventsPublisher")]
public class PlayerEventsPublisher : ScriptableObject
{
    public UnityEvent Respawn;
    public UnityEvent LeavingGround;
    public UnityEvent<Transform> EnteringGround;
    public UnityEvent<bool> Jumping;
    public UnityEvent<bool> Sliding;
    public UnityEvent<bool> AirSliding;
    public UnityEvent<bool> SlideJumping;
    public UnityEvent<bool> Dying;
    public UnityEvent<bool> InMiasma;
    public UnityEvent<bool> OnCliff;
    public UnityEvent<bool> GroundedState;
    public UnityEvent<bool> LandingToLower;
    public UnityEvent<float> LocomotionSpeed;
    public UnityEvent<bool> PauseGame;
    public UnityEvent<bool, float, float> ChangeBaseSpeed;
    public UnityEvent<bool, Transform> SetAutoslide;

}
