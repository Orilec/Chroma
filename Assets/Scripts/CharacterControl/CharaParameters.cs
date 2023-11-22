using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharaParameters : ScriptableObject
{
    [Header("Movement Parameters")] 
    public float maxMoveSpeed = 100f;
    public float baseMoveSpeed = 50f;
    public float speedIncrement = 2f;
    public float rotationSpeed = 15f;
    
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
}
