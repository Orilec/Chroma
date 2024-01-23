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
    public float maxSlopeFallSpeed = 3000f;
    public float distanceFromWall = 1.5f;
    [Range(0.0f, 1.0f)]public float facingWallSpeedMultiplier = 0.1f;
    
    [Header("Gravity")]
    public float gravityFallMin = -10f;
    public float gravityFallMax = -150f;
    public float gravityFallIncrementAmount = -20.0f;
    public float gravityFallIncrementTime = 0.05f;
    public float playerFallTimeMax = 0.3f;

    [Header("Miasma Parameters")] 
    public float miasmaTimeBeforeDeath = 5f;
    public float miasmaSpeed = 20f;
    public float miasmaGravity = -20f;
    
    [Header("Jump Parameters")] 
    public float initialJumpForce = 1500f;
    public float continualJumpForceMultiplier = 0.1f;
    public float jumpTime = 0.3f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.3f;
    
    [Header("Slide Parameters")] 
    public float slideNormalSpeed = 200f;
    public float slideTime = 1f;
    public float slideCooldownTime = 1f;
    public float slideSpeedDecrementAmount = 2f;
    public float slopeSlideMaxSpeed = 200f;
    public float slopeSlideIncrementAmount = 2f;
    public float slideBoostTime = 1f;
    public float initialSlideBoostForce = 15000f;
    public float continualSlideBoostForceMultiplier = 0.8f;
    
    [Header("Sliding Jump Parameters")] 
    public float slidingJumpVerticalForce = 1500f;
    public float slidingJumpHorizontalForce = 1500f;
    public float slidingJumpContinualMultiplier = 0.8f;
    public float slidingJumpBaseFallGravity = -0.01f;
    public float slidingJumpFallMultiplier = 8f;
    public float slidingJumpTime = 0.4f;
    [Range(0.0f, 1.0f)]public float slidingJumpHalfPointTime = 0.5f;
    public float slidingJumpBufferTime = 0.8f;

    [Header("Air Slide Parameters")] 
    public float airSlideTime = 0.2f;
    public float airSlideBaseForce = 1000f;
    public float airSlideForceMultiplier = 0.9f;
}
