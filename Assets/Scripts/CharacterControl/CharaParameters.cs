using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class CharaParameters : ScriptableObject
{
    [Header("Movement Parameters")] 
    public float maxMoveSpeed = 100f;
    public float baseMoveSpeed = 50f;
    public float speedIncrement = 2f;
    public float rotationSpeed = 15f;
    public float maxSlopeAngle = 48f;
    
    [Header("Gravity")]
    public float gravityFallMin = -10f;
    public float gravityFallMax = -150f;
    public float gravityFallIncrementAmount = -20.0f;
    public float gravityFallIncrementTime = 0.05f;
    public float playerFallTimeMax = 0.3f;

    [Header("Jump Parameters")] 
    public float initialJumpForce = 1500f;
    public float continualJumpForceMultiplier = 0.1f;
    public float jumpTime = 0.3f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.3f;
    
    [Header("Slide Parameters")] 
    public float slideSpeed = 200f;
    public float slideTime = 1f;
    public float slideSpeedDecrementAmount = 2f;
    public float slopeSlideMaxSpeed = 200f;
    public float slopeSlideIncrementAmount = 2f;
    
    [Header("Sliding Jump Parameters")] 
    public float slidingJumpVerticalForce = 1500f;
    public float slidingJumpHorizontalForce = 1500f;
    public float continualSlidingJumpForceMultiplier = 0.8f;
    public float slidingJumpTime = 0.4f;
    public float slidingJumpBufferTime = 0.8f;
}
