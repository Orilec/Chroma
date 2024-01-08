using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
[Header("References")] 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerTrailScript _trail;
    [SerializeField] private RespawnSystem _respawnSystem;

    [SerializeField] private CharaParameters _parameters;
    [SerializeField] private UIEventsPublisher _uiEventsPublisher;
    [SerializeField] private PlayerEventsPublisher _playerEventsPublisher;
    
    public IEnumerator AccelerationCoroutine;
    
    private Transform mainCam;

    private BouncePlatform _bouncePlatform;

    private const float ZeroF = 0f;
    private float _velocity, _jumpVelocity, _currentSpeed, _gravityFallCurrent;
    private bool _initialJump, _jumpWasPressedLastFrame, _slideWasPressedLastFrame, _isDownSlope, _isFacingWall, _canAirSlide, _isRespawning, _isFadingToBlack, _isInMiasma, _respawnWasPressedLastFrame;

    private Vector3 _movement;
    private Vector3 _playerMoveInput, _appliedMovement, _cameraRelativeMovement;

    private List<Timer> _timers;
    private CountdownTimer _jumpTimer, _playerFallTimer, _bounceFallTimer , _coyoteTimeCounter, _jumpBufferTimeCounter, _slideTimer, _slidingJumpTimer, _slidingJumpBufferCounter, _airSlideTimer, _bounceTimer, _miasmaTimer, _slideCooldownTimer;

    private StateMachine _stateMachine;
    
    //SIMPLE GETTERS
    public RespawnSystem RespawnSystem{ get { return _respawnSystem; } }
    public PlayerEventsPublisher PlayerEventsPublisher { get { return _playerEventsPublisher; } }
    public CountdownTimer JumpTimer { get { return _jumpTimer; } }
    public CountdownTimer PlayerFallTimer { get { return _playerFallTimer; } }
    public CountdownTimer CoyoteTimeCounter { get { return _coyoteTimeCounter; } }
    public CountdownTimer JumpBufferTimeCounter { get { return _jumpBufferTimeCounter; } }
    public CountdownTimer SlideTimer { get { return _slideTimer; } }
    public CountdownTimer SlidingJumpTimer { get { return _slidingJumpTimer; } }
    public CountdownTimer SlidingJumpBufferCounter { get { return _slidingJumpBufferCounter; } }
    public CountdownTimer AirSlideTimer { get { return _airSlideTimer; } }
    public CountdownTimer BounceTimer { get { return _bounceTimer; } }
    public CountdownTimer BounceFallTimer { get { return _bounceFallTimer; } }
    public CountdownTimer MiasmaTimer { get { return _miasmaTimer; } }
    public CountdownTimer SlideCooldownTimer { get { return _slideCooldownTimer; } }
    public GroundCheck GroundCheck{ get { return _groundCheck; } }
    public Rigidbody Rigidbody{ get { return _rigidbody; } }
    public PlayerTrailScript Trail{ get { return _trail; } }
    public BouncePlatform CurrentBouncePlatform{ get { return _bouncePlatform; } }
    public float GravityFallMin { get { return _parameters.gravityFallMin; } }
    public float GravityFallMax { get { return _parameters.gravityFallMax; } }
    public float GravityFallIncrementAmount { get { return _parameters.gravityFallIncrementAmount; } }
    public float GravityFallIncrementTime { get { return _parameters.gravityFallIncrementTime; } }
    public float PlayerFallTimeMax { get { return _parameters.playerFallTimeMax; } }
    public float SlideNormalSpeed { get { return _parameters.slideNormalSpeed; } }
    public float MaxMoveSpeed { get { return _parameters.maxMoveSpeed; } }
    public float BaseMoveSpeed { get { return _parameters.baseMoveSpeed; } }
    public float MoveSpeedIncrement { get { return _parameters.speedIncrement; } }
    public float MiasmaSpeed { get { return _parameters.miasmaSpeed; } }
    public float SlideSpeedDecrementAmount { get { return _parameters.slideSpeedDecrementAmount; } }
    public float SlopeSlideMaxSpeed { get { return _parameters.slopeSlideMaxSpeed; } }
    public float SlopeSlideSpeedIncrementAmount { get { return _parameters.slopeSlideIncrementAmount; } }
    public float SlidingJumpVerticalForce { get { return _parameters.slidingJumpVerticalForce; } }
    public float SlidingJumpHorizontalForce { get { return _parameters.slidingJumpHorizontalForce; } }
    public float SlidingJumpContinualMultiplier { get { return _parameters.slidingJumpContinualMultiplier; } }
    public float SlidingJumpFallMultiplier { get { return _parameters.slidingJumpFallMultiplier; } }
    public float SlidingJumpHalfPointTime { get { return _parameters.slidingJumpHalfPointTime; } }
    public float SlidingJumpBaseFallGravity { get { return _parameters.slidingJumpBaseFallGravity; } }
    public float AirSlideBaseForce { get { return _parameters.airSlideBaseForce; } }
    public float AirSlideForceMultiplier { get { return _parameters.airSlideForceMultiplier; } }
    public float MiasmaGravity { get { return _parameters.miasmaGravity; } }
    public bool IsDownSlope { get { return _isDownSlope; } }
    
    
    
    //GETTERS + SETTERS
    public float PlayerMoveInputY { get { return _playerMoveInput.y; } set { _playerMoveInput.y = value; } }
    public Vector3 PlayerMoveInput { get { return _playerMoveInput; } set { _playerMoveInput = value; } }
    public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
    public float InitialJumpForce { get { return _parameters.initialJumpForce; }set { _parameters.initialJumpForce = value; } }
    public float ContinualJumpForceMultiplier { get { return _parameters.continualJumpForceMultiplier; }set { _parameters.continualJumpForceMultiplier = value; } }
    public float GravityFallCurrent { get { return _gravityFallCurrent; }set { _gravityFallCurrent = value; } }
    public bool InitialJump { get { return _initialJump;} set { _initialJump = value; } }
    public bool JumpWasPressedLastFrame { get { return _jumpWasPressedLastFrame;} set { _jumpWasPressedLastFrame = value; } }
    public bool SlideWasPressedLastFrame { get { return _slideWasPressedLastFrame;} set { _slideWasPressedLastFrame = value; } }
    public bool CanAirSlide { get { return _canAirSlide;} set { _canAirSlide = value; } }
    public bool IsRespawning { get { return _isRespawning;} set { _isRespawning = value; } }
    public bool IsFadingToBlack { get { return _isFadingToBlack;} set { _isFadingToBlack = value; } }
    public bool IsInMiasma { get { return _isInMiasma;} set { _isInMiasma = value; } }

    


    private void Awake()
    {
        mainCam = Camera.main.transform;
        
        _rigidbody.freezeRotation = true;
        
        //Timers setup
        _jumpTimer = new CountdownTimer(_parameters.jumpTime);
        _playerFallTimer = new CountdownTimer(_parameters.playerFallTimeMax);
        _coyoteTimeCounter = new CountdownTimer(_parameters.coyoteTime);
        _jumpBufferTimeCounter = new CountdownTimer(_parameters.jumpBufferTime);
        _slideTimer = new CountdownTimer(_parameters.slideTime);
        _slidingJumpTimer = new CountdownTimer(_parameters.slidingJumpTime);
        _slidingJumpBufferCounter = new CountdownTimer(_parameters.slidingJumpBufferTime);
        _airSlideTimer = new CountdownTimer(_parameters.airSlideTime);
        _bounceTimer = new CountdownTimer(_parameters.jumpTime);
        _bounceFallTimer = new CountdownTimer(0f);
        _miasmaTimer = new CountdownTimer(_parameters.miasmaTimeBeforeDeath);
        _slideCooldownTimer = new CountdownTimer(_parameters.slideCooldownTime);
        
        _timers = new List<Timer> { _jumpTimer, _playerFallTimer, _coyoteTimeCounter, _jumpBufferTimeCounter, _slideTimer, _slidingJumpTimer, _slidingJumpBufferCounter, _airSlideTimer, _bounceTimer, _bounceFallTimer, _miasmaTimer, _slideCooldownTimer };
        
        //_jumpTimer.OnTimerStop += () => ;
        
        // State Machine creation
        _stateMachine = new StateMachine();
        
        // States creation
        var groundedState = new GroundedState(this, _input);
        var jumpState = new JumpState(this, _input);
        var fallState = new FallState(this, _input);
        var slideState = new SlideState(this, _input);
        var slidingJumpState = new SlidingJumpState(this, _input);
        var airSlideState = new AirSlideState(this, _input);
        var bounceState = new BounceState(this, _input);
        var bounceFallState = new BounceFallState(this, _input);
        var miasmaState = new MiasmaState(this, _input);
        var respawningState = new RespawningState(this, _input);
        var groundedEnvironmentState = new GroundedOnEnvironmentState(this, _input);
        
        // Transitions creation
        At(groundedState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(groundedState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning && !_groundCheck.IsGrounded));
        At(groundedState, slideState, new FuncPredicate(()=> _slideTimer.IsRunning));
        At(groundedState,slidingJumpState, new FuncPredicate(()=> _slidingJumpTimer.IsRunning));
        At(groundedState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(jumpState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning));
        At(jumpState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        
        At(fallState, groundedState, new FuncPredicate(()=> _groundCheck.IsGrounded));
        At(fallState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(fallState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        At(fallState, bounceState, new FuncPredicate(()=> _bounceTimer.IsRunning));
        At(fallState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(bounceFallState, groundedState, new FuncPredicate(()=> _groundCheck.IsGrounded));
        At(bounceFallState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(bounceFallState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        At(bounceFallState, bounceState, new FuncPredicate(()=> _bounceTimer.IsRunning));
        At(bounceFallState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(slideState, groundedState, new FuncPredicate(()=> !_slideTimer.IsRunning && _groundCheck.IsGrounded && !_isDownSlope));
        At(slideState, fallState, new FuncPredicate(()=>  !_groundCheck.IsGrounded));
        At(slideState, slidingJumpState, new FuncPredicate(()=>  _slidingJumpTimer.IsRunning));
        At(slideState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(slidingJumpState, groundedState, new FuncPredicate(()=> !_slidingJumpTimer.IsRunning && _groundCheck.IsGrounded));
        At(slidingJumpState, fallState, new FuncPredicate(()=> !_slidingJumpTimer.IsRunning && !_groundCheck.IsGrounded));
        
        At(airSlideState, fallState, new FuncPredicate(()=> !_airSlideTimer.IsRunning));

        At(bounceState, bounceFallState, new FuncPredicate(()=> !_bounceTimer.IsRunning));
        
        At(miasmaState, groundedState, new FuncPredicate(()=> !_isInMiasma));
        At(miasmaState, respawningState, new FuncPredicate(()=> !_miasmaTimer.IsRunning && _isInMiasma ));
        
        At(respawningState, fallState, new FuncPredicate(()=> !_isRespawning));
        Any(respawningState, new FuncPredicate(()=> _isRespawning));
        
        // At(groundedEnvironmentState, fallState, new FuncPredicate(()=> !_groundCheck.IsOnEnvironment));
        // Any(groundedEnvironmentState, new FuncPredicate(()=> _groundCheck.IsOnEnvironment));
        
        // Set Initial State
        _stateMachine.SetState(groundedState);
        
        //Set events

        
        _uiEventsPublisher.FadeToBlackFinished.AddListener(StopRespawning);
        _uiEventsPublisher.FirstFadeFinished.AddListener(Fading);
        
        //Set coroutines
        AccelerationCoroutine = Accelerate(0f, 0f);
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    
    
    private void Update()
    {
        _stateMachine.Update();
        HandleTimers();
        DebugRespawn();
    }

    private void DebugRespawn()
    {
        if (_input.DebugRespawn && ! _respawnWasPressedLastFrame)
        {
            _isRespawning = true;
        }

        _respawnWasPressedLastFrame = _input.DebugRespawn;
    }

    private void FixedUpdate()
    {
        _playerMoveInput = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);
 
        _stateMachine.FixedUpdate();
        
        PlayerSlope();
        
        _rigidbody.AddForce(_playerMoveInput, ForceMode.Force);

    }

    private void HandleTimers()
    {
        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
    

    private void StopRespawning()
    {
        IsRespawning = false;
        _isFadingToBlack = false;
    }
    private void Fading()
    {
        _isFadingToBlack = true;
    }
    
    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;

        Vector3 cameraForward = mainCam.forward;
        Vector3 cameraRight = mainCam.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }
    
    public void PlayerMove()
    {
        Vector3 calculatedPlayerMovement = (new Vector3(_playerMoveInput.x * _currentSpeed * _rigidbody.mass,
            _playerMoveInput.y * _rigidbody.mass,
            _playerMoveInput.z * _currentSpeed * _rigidbody.mass));
        
        _playerMoveInput = calculatedPlayerMovement;
        _playerMoveInput = PlayerFacingWall();
        _playerMoveInput = ConvertToCameraSpace(_playerMoveInput);
    }

    private Vector3 PlayerFacingWall()
    {
        var calculatedPlayerMovement = _playerMoveInput;
        
        _isFacingWall = Physics.Raycast(transform.position, transform.forward, _parameters.distanceFromWall);
        if (_isFacingWall && !_groundCheck.IsGrounded)
        {
            calculatedPlayerMovement = new Vector3(_playerMoveInput.x * _parameters.facingWallSpeedMultiplier, _playerMoveInput.y, _playerMoveInput.z * _parameters.facingWallSpeedMultiplier);
        }
        return calculatedPlayerMovement;

    }

    private void PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = _playerMoveInput;

        if (_groundCheck.IsGrounded)
        {
            Vector3 localGroundCheckHitNormal = _groundCheck.GroundCheckHit.normal;
            float groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, _rigidbody.transform.up);
            if (groundSlopeAngle != 0f)
            {
                Quaternion slopeAngleRotation = Quaternion.FromToRotation(_rigidbody.transform.up, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation *  calculatedPlayerMovement;
                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, _rigidbody.transform.up) - 90f;
                _isDownSlope = relativeSlopeAngle > 0;
            }
            else
            {
                _isDownSlope = false;
            }
            if ((groundSlopeAngle >= _parameters.maxSlopeAngle && !_isDownSlope) || (_groundCheck.IsOnEnvironment && !_isDownSlope))
            {
                calculatedPlayerMovement = new Vector3(localGroundCheckHitNormal.x, localGroundCheckHitNormal.y * 90f, localGroundCheckHitNormal.z).normalized * (-1 * _parameters.maxSlopeFallSpeed);
                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, _rigidbody.transform.up) - 90f;
                _isDownSlope = relativeSlopeAngle > 0;
            }
        }
        _playerMoveInput = calculatedPlayerMovement;
    }
    
    public void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _playerMoveInput.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = _playerMoveInput.z;

        Quaternion currentRotation = transform.rotation;
        if (_input.MoveInput.magnitude > ZeroF && positionToLookAt != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _parameters.rotationSpeed * Time.deltaTime);
        }
    }
    
    public IEnumerator Decelerate(float desiredSpeed, float decrementAmount)
    {
        while (_currentSpeed > desiredSpeed)
        {
            _currentSpeed -= decrementAmount;
            yield return new WaitForEndOfFrame();
        }
    }   
    
    public IEnumerator Accelerate(float desiredSpeed, float incrementAmount)
    {
        while (_currentSpeed < desiredSpeed)
        {
            _currentSpeed += incrementAmount;
            yield return new WaitForEndOfFrame();
        }
    }

    public void UpdateBouncePlatform(BouncePlatform newPlatform)
    {
        _bounceTimer.Reset(newPlatform.BounceTime);
        _bouncePlatform = newPlatform;
        _bounceTimer.Start();
    }

    public void GetCollectible(int index)
    {
        _playerEventsPublisher.CardCollected.Invoke(index);
    }
}
