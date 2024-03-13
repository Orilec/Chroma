using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField] private LayerMask _environmentLayers;
    [SerializeField] private CharaParameters _parameters;
    
    [Header("Event Publishers")] 
    [SerializeField] private UIEventsPublisher _uiEventsPublisher;
    [SerializeField] private PlayerEventsPublisher _playerEventsPublisher;
    
    [Header("Debug")] 
    [SerializeField] private bool drawGizmo; 
    [Range(1, 20)]
    [SerializeField] private float scaleX = 1f;
    [Range(1, 20)]
    [SerializeField] private float scaleY = 1f;

    public IEnumerator AccelerationCoroutine, DecelerationCoroutine;
    
    //References
    private Transform _mainCam;
    private Rigidbody _rigidbody;
    private GroundCheck _groundCheck;
    private ThrowSystem _throwSystem;
    private InputReader _input;
    private PlayerTrailScript _trail;
    private RespawnSystem _respawnSystem;
    private BouncePlatform _bouncePlatform;

    private const float ZeroF = 0f;
    private float _velocity, _jumpVelocity, _currentSpeed, _gravityFallCurrent, _relativeCurrentSpeed, _leavingGroundY, _currentBaseSpeed, _currentMaxSpeed;
    private bool _initialJump, _jumpWasPressedLastFrame, _slideWasPressedLastFrame, _isDownSlope, _isFacingWall, _canAirSlide, _isRespawning, _isFadingToBlack, _isInMiasma, _respawnWasPressedLastFrame, _isOnSlope, _isAutoSliding, _wasSlideJumping;
    private int _stepsSinceGrounded;
    private Vector3 _movement;
    private Vector3 _playerMoveInput, _appliedMovement, _cameraRelativeMovement, _localGroundCheckHitNormal;

    private List<Timer> _timers;
    private CountdownTimer _jumpTimer, _playerFallTimer, _bounceFallTimer , _coyoteTimeCounter, _jumpBufferTimeCounter, _slideTimer, _slidingJumpTimer, _slidingJumpBufferCounter, _airSlideTimer, _bounceTimer, _miasmaTimer, _slideCooldownTimer, _slideBoostTimer, _jumpMinTimer;

    private StateMachine _stateMachine;
    
    //SIMPLE REFERENCES GETTERS 
    public RespawnSystem RespawnSystem{ get { return _respawnSystem; } }
    public ThrowSystem ThrowSystem{ get { return _throwSystem; } }
    public Vector3 GroundCheckHitNormal{ get { return _localGroundCheckHitNormal; } }
    public GroundCheck GroundCheck{ get { return _groundCheck; } }
    public Rigidbody Rigidbody{ get { return _rigidbody; } }
    public PlayerTrailScript Trail{ get { return _trail; } }
    public BouncePlatform CurrentBouncePlatform{ get { return _bouncePlatform; } }
    
    //TIMERS GETTERS 
    public CountdownTimer JumpTimer { get { return _jumpTimer; } }
    public CountdownTimer JumpMinTimer { get { return _jumpMinTimer; } }
    public CountdownTimer PlayerFallTimer { get { return _playerFallTimer; } }
    public CountdownTimer CoyoteTimeCounter { get { return _coyoteTimeCounter; } }
    public CountdownTimer JumpBufferTimeCounter { get { return _jumpBufferTimeCounter; } }
    public CountdownTimer SlideTimer { get { return _slideTimer; } }
    public CountdownTimer SlidingJumpTimer { get { return _slidingJumpTimer; } }
    public CountdownTimer SlidingJumpBufferCounter { get { return _slidingJumpBufferCounter; } }
    public CountdownTimer AirSlideTimer { get { return _airSlideTimer; } }
    public CountdownTimer SlideBoostTimer { get { return _slideBoostTimer; } }
    public CountdownTimer BounceFallTimer { get { return _bounceFallTimer; } }
    public CountdownTimer MiasmaTimer { get { return _miasmaTimer; } }
    public CountdownTimer SlideCooldownTimer { get { return _slideCooldownTimer; } }
    
    
    //PARAMETERS GETTERS 
    public float GravityFallMin { get { return _parameters.gravityFallMin; } }
    public float GravityFallMax { get { return _parameters.gravityFallMax; } }
    public float GravityFallIncrementAmount { get { return _parameters.gravityFallIncrementAmount; } }
    public float GravityFallIncrementTime { get { return _parameters.gravityFallIncrementTime; } }
    public float PlayerFallTimeMax { get { return _parameters.playerFallTimeMax; } }
    public float SlideNormalSpeed { get { return _parameters.slideNormalSpeed; } }
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
    public float InitialSlideBoostForce { get { return _parameters.initialSlideBoostForce; } }
    public float ContinualSlideBoostForceMultiplier { get { return _parameters.continualSlideBoostForceMultiplier; } }
    public float MiasmaGravity { get { return _parameters.miasmaGravity; } }
    public bool IsDownSlope { get { return _isDownSlope; } }
    
    
    
    //GETTERS + SETTERS
    public float PlayerMoveInputY { get { return _playerMoveInput.y; } set { _playerMoveInput.y = value; } }
    public float PlayerMoveInputZ { get { return _playerMoveInput.z; } set { _playerMoveInput.z = value; } }
    public Vector3 PlayerMoveInput { get { return _playerMoveInput; } set { _playerMoveInput = value; } }
    public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
    public float CurrentBaseSpeed { get { return _currentBaseSpeed; } set { _currentBaseSpeed = value; } }
    public float CurrentMaxSpeed { get { return _currentMaxSpeed; } set { _currentMaxSpeed = value; } }
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
    public bool IsAutoSliding { get { return _isAutoSliding;} set { _isAutoSliding = value; } }
    public bool WasSlideJumping { get { return _wasSlideJumping;} set { _wasSlideJumping = value; } }

    
    //State machine utility functions
    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    
    
    //Timer update
    private void HandleTimers()
    {
        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
    
    private void Awake()
    {
        //Filling references
        _mainCam = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundCheck>();
        _throwSystem = GetComponent<ThrowSystem>();
        _input = ChroManager.GetManager<InputReader>();
        _trail = FindObjectOfType<PlayerTrailScript>();
        _respawnSystem = GetComponent<RespawnSystem>();
        
        //Subscribing to manager
        ChroManager.GetManager<PlayerManager>().SubscribePlayer(this);
        
        _rigidbody.freezeRotation = true;
        
        //Timers setup
        _jumpTimer = new CountdownTimer(_parameters.jumpTime);
        _jumpMinTimer = new CountdownTimer(_parameters.jumpMinTime);
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
        _slideBoostTimer = new CountdownTimer(_parameters.slideBoostTime);
        
        _timers = new List<Timer> { _jumpTimer,_jumpMinTimer, _playerFallTimer, _coyoteTimeCounter, _jumpBufferTimeCounter, _slideTimer, _slidingJumpTimer, _slidingJumpBufferCounter, _airSlideTimer, _bounceTimer, _bounceFallTimer, _miasmaTimer, _slideCooldownTimer, _slideBoostTimer };
        
        //_jumpTimer.OnTimerStop += () => ;
        
        // State Machine creation
        _stateMachine = new StateMachine();
        
        // States creation
        var groundedState = new GroundedState(this, _input, _playerEventsPublisher);
        var jumpState = new JumpState(this, _input, _playerEventsPublisher);
        var fallState = new FallState(this, _input, _playerEventsPublisher);
        var slideState = new SlideState(this, _input, _playerEventsPublisher);
        var slidingJumpState = new SlidingJumpState(this, _input, _playerEventsPublisher);
        var airSlideState = new AirSlideState(this, _input, _playerEventsPublisher);
        var bounceState = new BounceState(this, _input, _playerEventsPublisher);
        var bounceFallState = new BounceFallState(this, _input, _playerEventsPublisher);
        var miasmaState = new MiasmaState(this, _input, _playerEventsPublisher);
        var respawningState = new RespawningState(this, _input, _playerEventsPublisher);
        var slideBoostState = new SlideBoostState(this, _input, _playerEventsPublisher);
        var autoSlideState = new AutoSlideState(this, _input, _playerEventsPublisher);
        
        // Transitions creation
        At(groundedState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(groundedState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning && !_groundCheck.IsGrounded && _stepsSinceGrounded > 1));
        At(groundedState, slideState, new FuncPredicate(()=> _slideTimer.IsRunning));
        At(groundedState,slidingJumpState, new FuncPredicate(()=> _slidingJumpTimer.IsRunning));
        At(groundedState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        At(groundedState, autoSlideState, new FuncPredicate(()=> _isOnSlope && _groundCheck.AutoSlide));
        
        At(jumpState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning && !_jumpMinTimer.IsRunning));
        At(jumpState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        
        At(fallState, groundedState, new FuncPredicate(()=> _groundCheck.IsGrounded));
        At(fallState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(fallState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        At(fallState, bounceState, new FuncPredicate(()=> _bounceTimer.IsRunning));
        At(fallState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        At(fallState, autoSlideState, new FuncPredicate(()=> _groundCheck.IsGrounded && _isOnSlope && _groundCheck.AutoSlide));
        
        At(bounceFallState, groundedState, new FuncPredicate(()=> _groundCheck.IsGrounded));
        At(bounceFallState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(bounceFallState, airSlideState, new FuncPredicate(()=> _airSlideTimer.IsRunning));
        At(bounceFallState, bounceState, new FuncPredicate(()=> _bounceTimer.IsRunning));
        At(bounceFallState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(slideState, groundedState, new FuncPredicate(()=> !_slideTimer.IsRunning && _groundCheck.IsGrounded && !_isDownSlope));
        At(slideState, autoSlideState, new FuncPredicate(()=> _groundCheck.IsGrounded && _isOnSlope && _groundCheck.AutoSlide));
        At(slideState, fallState, new FuncPredicate(()=>  !_groundCheck.IsGrounded && _stepsSinceGrounded > 1));
        At(slideState, slidingJumpState, new FuncPredicate(()=>  _slidingJumpTimer.IsRunning));
        At(slideState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        
        At(slidingJumpState, groundedState, new FuncPredicate(()=> !_slidingJumpTimer.IsRunning && _groundCheck.IsGrounded));
        At(slidingJumpState, fallState, new FuncPredicate(()=> !_slidingJumpTimer.IsRunning && !_groundCheck.IsGrounded));
        At(slidingJumpState, autoSlideState, new FuncPredicate(()=> !_slidingJumpTimer.IsRunning && _groundCheck.IsGrounded && _isOnSlope && _groundCheck.AutoSlide));
        
        At(airSlideState, fallState, new FuncPredicate(()=> !_airSlideTimer.IsRunning));

        At(bounceState, bounceFallState, new FuncPredicate(()=> !_bounceTimer.IsRunning));
        
        At(miasmaState, groundedState, new FuncPredicate(()=> !_isInMiasma));
        At(miasmaState, respawningState, new FuncPredicate(()=> !_miasmaTimer.IsRunning && _isInMiasma ));
        
        At(respawningState, fallState, new FuncPredicate(()=> !_isRespawning));
        Any(respawningState, new FuncPredicate(()=> _isRespawning));
        
        At(autoSlideState, fallState, new FuncPredicate(()=>  !_groundCheck.IsGrounded && _stepsSinceGrounded > 1 || !_isDownSlope && !_groundCheck.IsGrounded));
        At(autoSlideState, groundedState, new FuncPredicate(()=>  !_groundCheck.AutoSlide && _groundCheck.IsGrounded || !_isDownSlope && _groundCheck.IsGrounded));
        At(autoSlideState, slidingJumpState, new FuncPredicate(()=>  _slidingJumpTimer.IsRunning));
        At(autoSlideState, miasmaState, new FuncPredicate(()=> _miasmaTimer.IsRunning));
        At(autoSlideState, slideBoostState, new FuncPredicate(()=> _slideBoostTimer.IsRunning));
        
        At(slideBoostState, fallState, new FuncPredicate(()=> !_slideBoostTimer.IsRunning));
        
        // At(groundedEnvironmentState, fallState, new FuncPredicate(()=> !_groundCheck.IsOnEnvironment));
        // Any(groundedEnvironmentState, new FuncPredicate(()=> _groundCheck.IsOnEnvironment));
        
        // Set Initial State
        _stateMachine.SetState(groundedState);
        
        //Set event listeners
        _uiEventsPublisher.FadeToBlackFinished.AddListener(StopRespawning);
        _uiEventsPublisher.FirstFadeFinished.AddListener(Fading);
        
        _playerEventsPublisher.LeavingGround.AddListener(SaveYPos);
        _playerEventsPublisher.EnteringGround.AddListener(CompareYPos);
        
        _playerEventsPublisher.ChangeBaseSpeed.AddListener(ChangeMoveSpeed);
        
        //Setup coroutines
        AccelerationCoroutine = Accelerate(0f, 0f);
        DecelerationCoroutine = Decelerate(0f, 0f);
        
        //Setup speed variables
        _currentBaseSpeed = BaseMoveSpeed;
        _currentMaxSpeed = _parameters.maxMoveSpeed;
    }

    
    
    //Update and fixed update functions
    private void Update()
    {
        _stateMachine.Update();
        HandleTimers();
        DebugRespawn();
    }
    
    private void FixedUpdate()
    {
        _playerMoveInput = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);
 
        _stateMachine.FixedUpdate();
        
        PlayerFacingWall();
        PlayerSlope();
        PlayerBumps();
        SnapToGround();
        
        _rigidbody.AddForce(_playerMoveInput, ForceMode.Force);

    }
    
    
    //Respawning functions
    private void DebugRespawn()
    {
        if (_input.DebugRespawn && ! _respawnWasPressedLastFrame)
        {
            _isRespawning = true;
        }
        _respawnWasPressedLastFrame = _input.DebugRespawn;
    }
    public void Respawn()
    {
        _isRespawning = true;
    }
    private void StopRespawning()
    {
        IsRespawning = false;
        _isFadingToBlack = false;
    }
    
    

    //Input and movement handling functions
    private Vector3 ConvertToTransformSpace(Transform directionTransform, Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;

        Vector3 slopeDirectionForward = directionTransform.forward;
        Vector3 slopeDirectionRight = directionTransform.right;

        slopeDirectionForward.y = 0;
        slopeDirectionRight.y = 0;

        slopeDirectionForward = slopeDirectionForward.normalized;
        slopeDirectionRight = slopeDirectionRight.normalized;

        Vector3 forwardZProduct = vectorToRotate.z * slopeDirectionForward;
        Vector3 rightXProduct = vectorToRotate.x * slopeDirectionRight;

        Vector3 vectorRotatedToSlopeDirection = forwardZProduct + rightXProduct;
        vectorRotatedToSlopeDirection.y = currentYValue;
        return vectorRotatedToSlopeDirection;
    }
    
    public void PlayerMove()
    {
        Vector3 calculatedPlayerMovement = (new Vector3(_playerMoveInput.x * _currentSpeed * _rigidbody.mass,
            _playerMoveInput.y * _rigidbody.mass,
            _playerMoveInput.z * _currentSpeed * _rigidbody.mass));
        
        _playerMoveInput = calculatedPlayerMovement;
        if (!_isAutoSliding) _playerMoveInput = ConvertToTransformSpace(_mainCam.transform, _playerMoveInput);
        else _playerMoveInput = ConvertToTransformSpace(_groundCheck.slopeDirection, _playerMoveInput);

        if (_input.MoveInput.magnitude <= 0) _relativeCurrentSpeed = 0f;
        else _relativeCurrentSpeed = (_currentSpeed - _currentBaseSpeed ) / (_parameters.maxMoveSpeed - BaseMoveSpeed);
        _playerEventsPublisher.LocomotionSpeed.Invoke(_relativeCurrentSpeed);
    }

    private void PlayerFacingWall()
    {
        var calculatedPlayerMovement = _playerMoveInput;
        
        _isFacingWall = Physics.Raycast(transform.position, transform.forward, _parameters.distanceFromWall, _environmentLayers);
        if (_isFacingWall && !_groundCheck.IsGrounded)
        {
            calculatedPlayerMovement = new Vector3(_playerMoveInput.x * _parameters.facingWallSpeedMultiplier, _playerMoveInput.y, _playerMoveInput.z * _parameters.facingWallSpeedMultiplier);
        }
        _playerMoveInput = calculatedPlayerMovement;
    }

    private void PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = _playerMoveInput;

        if (_groundCheck.IsGrounded)
        {
            _localGroundCheckHitNormal = _groundCheck.GroundCheckHit.normal;
            float groundSlopeAngle = Vector3.Angle(_localGroundCheckHitNormal, _rigidbody.transform.up);
            if (groundSlopeAngle != 0f)
            {
                Quaternion slopeAngleRotation = Quaternion.FromToRotation(_rigidbody.transform.up, _localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation *  calculatedPlayerMovement;
                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, _rigidbody.transform.up) - 90f;
                _isDownSlope = relativeSlopeAngle > 0;
                _isOnSlope = true;
            }
            else
            {
                _isOnSlope = false;
                _isDownSlope = false;
            }
            
            if ((groundSlopeAngle >= _parameters.maxSlopeAngle && !_isDownSlope) || (_groundCheck.IsOnEnvironment && !_isDownSlope))
            {
                calculatedPlayerMovement = new Vector3(_localGroundCheckHitNormal.x, _localGroundCheckHitNormal.y * 90f, _localGroundCheckHitNormal.z).normalized * (-1 * _parameters.maxSlopeFallSpeed);
                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, _rigidbody.transform.up) - 90f;
                _isDownSlope = relativeSlopeAngle > 0;
            }
        }
        _playerMoveInput = calculatedPlayerMovement;
    }
    
    private void PlayerBumps()
    {
        _stepsSinceGrounded += 1;
        if (_groundCheck.IsGrounded) _stepsSinceGrounded = 0;
    }
    
    private void SnapToGround()
    {
        if (_groundCheck.IsGrounded) return;
        if (_jumpTimer.IsRunning) return;
        if (_stepsSinceGrounded > 1) return;
        if (!_groundCheck.GroundBelow) return;
        _playerMoveInput = new Vector3(_playerMoveInput.x,-10000f, _playerMoveInput.z);
        _groundCheck.IsGrounded = true;
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
    
    
    //Acceleration and Deceleration coroutines
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
    

    //Other functions
    public void UpdateBouncePlatform(BouncePlatform newPlatform)
    {
        _bounceTimer.Reset(newPlatform.BounceTime);
        _bouncePlatform = newPlatform;
        _bounceTimer.Start();
    }
    
    private void SaveYPos()
    {
        _leavingGroundY = transform.position.y;
    }

    private void CompareYPos()
    {
        float fallHeight = transform.position.y - _leavingGroundY;
        var lowerGround = fallHeight < -1f;
        _playerEventsPublisher.LandingToLower.Invoke(lowerGround);

        if (fallHeight < -_parameters.respawningFallHeight)
        {
            _isRespawning = true;
        }
    }
    
    private void Fading()
    {
        _isFadingToBlack = true;
    }

    private void ChangeMoveSpeed(bool change, float newBaseSpeed, float newMaxSpeed)
    {
        if (change)
        {
            _currentBaseSpeed = newBaseSpeed;
            _currentMaxSpeed = newMaxSpeed;
            _currentSpeed = _currentBaseSpeed;
        }
        else
        {
            _currentBaseSpeed = BaseMoveSpeed;
            _currentMaxSpeed = _parameters.maxMoveSpeed;
        }
    }


    //Visual debug
    private void OnDrawGizmos()
    {
        if (!drawGizmo) return; 
        Gizmos.color = new Color(0, 0.3f, 1f, 1f);

        Gizmos.DrawWireCube(transform.position - new Vector3(0f, _parameters.respawningFallHeight/2 + transform.localScale.y, 0f) , new Vector3(2f * scaleX, _parameters.respawningFallHeight, 2f * scaleY));

        Gizmos.color = new Color(0, 0.3f, 1f, 0.2f);
        Gizmos.DrawCube(transform.position - new Vector3(0f, _parameters.respawningFallHeight/2 + transform.localScale.y, 0f) , new Vector3(2f * scaleX, _parameters.respawningFallHeight, 2f * scaleY));
    }

}
