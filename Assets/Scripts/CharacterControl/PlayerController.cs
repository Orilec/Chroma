using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
[Header("References")] 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private CinemachineFreeLook _freeLookCam;
    [SerializeField] private InputReader _input;

    [SerializeField] private CharaParameters _parameters;
    
    public event UnityAction LeavingGround = delegate {  };
    public event UnityAction EnteringGround = delegate {  };
    
    private Transform mainCam;

    private const float ZeroF = 0f;
    private float _velocity, _jumpVelocity, _currentMoveSpeed, _gravityFallCurrent;
    private bool _initialJump, _jumpWasPressedLastFrame;

    private Vector3 _movement;
    private Vector3 _playerMoveInput, _appliedMovement, _cameraRelativeMovement ;

    private List<Timer> _timers;
    private CountdownTimer _jumpTimer;
    private CountdownTimer _playerFallTimer , _coyoteTimeCounter, _jumpBufferTimeCounter;

    private StateMachine _stateMachine;
    
    //SIMPLE GETTERS
    public CountdownTimer JumpTimer { get { return _jumpTimer; } }
    public CountdownTimer PlayerFallTimer { get { return _playerFallTimer; } }
    public CountdownTimer CoyoteTimeCounter { get { return _coyoteTimeCounter; } }
    public CountdownTimer JumpBufferTimeCounter { get { return _jumpBufferTimeCounter; } }
    public GroundCheck GroundCheck{ get { return _groundCheck; } }
    public Rigidbody Rigidbody{ get { return _rigidbody; } }
    public float GravityFallMin { get { return _parameters.gravityFallMin; } }
    public float GravityFallMax { get { return _parameters.gravityFallMax; } }
    public float GravityFallIncrementAmount { get { return _parameters.gravityFallIncrementAmount; } }
    public float GravityFallIncrementTime { get { return _parameters.gravityFallIncrementTime; } }
    public float PlayerFallTimeMax { get { return _parameters.playerFallTimeMax; } }
    
    
    
    //GETTERS + SETTERS
    public float PlayerMoveInputY { get { return _playerMoveInput.y; } set { _playerMoveInput.y = value; } }
    public float InitialJumpForce { get { return _parameters.initialJumpForce; }set { _parameters.initialJumpForce = value; } }
    public float ContinualJumpForceMultiplier { get { return _parameters.continualJumpForceMultiplier; }set { _parameters.continualJumpForceMultiplier = value; } }
    public float GravityFallCurrent { get { return _gravityFallCurrent; }set { _gravityFallCurrent = value; } }
    public bool InitialJump { get { return _initialJump;} set { _initialJump = value; } }
    public bool JumpWasPressedLastFrame { get { return _jumpWasPressedLastFrame;} set { _jumpWasPressedLastFrame = value; } }


    private void Awake()
    {
        mainCam = Camera.main.transform;
        
        _rigidbody.freezeRotation = true;
        
        //Timers setup
        _jumpTimer = new CountdownTimer(_parameters.jumpTime);

        _playerFallTimer = new CountdownTimer(_parameters.playerFallTimeMax);
        _coyoteTimeCounter = new CountdownTimer(_parameters.coyoteTime);
        _jumpBufferTimeCounter = new CountdownTimer(_parameters.jumpBufferTime);
        
        _timers = new List<Timer> { _jumpTimer, _playerFallTimer, _coyoteTimeCounter, _jumpBufferTimeCounter };
        
        //_jumpTimer.OnTimerStop += () => ;
        
        // State Machine creation
        _stateMachine = new StateMachine();
        
        // States creation
        var groundedState = new GroundedState(this, _input);
        var jumpState = new JumpState(this, _input);
        var fallState = new FallState(this, _input);
        
        // Transitions creation
        At(groundedState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        At(groundedState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning && !_groundCheck.IsGrounded));
        
        At(jumpState, fallState, new FuncPredicate(()=> !_jumpTimer.IsRunning));
        
        At(fallState, groundedState, new FuncPredicate(()=> _groundCheck.IsGrounded));
        At(fallState, jumpState, new FuncPredicate(()=> _jumpTimer.IsRunning));
        
        
        // Set Initial State
        _stateMachine.SetState(groundedState);
        
        //Set events
        _groundCheck.LeavingGround += OnLeavingGround;
        _groundCheck.EnteringGround += OnEnteringGround;
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    private void Start()
    {

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    
    private void Update()
    {
        _stateMachine.Update();
        HandleTimers();
    }

    private void FixedUpdate()
    {
        _playerMoveInput = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);
 
        _stateMachine.FixedUpdate();
        
        _appliedMovement = _cameraRelativeMovement;
        
        _rigidbody.AddForce(_appliedMovement, ForceMode.Force);

    }

    private void HandleTimers()
    {
        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    private void OnLeavingGround()
    {
        LeavingGround.Invoke();
    }
    private void OnEnteringGround()
    {
        EnteringGround.Invoke();
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
        if (_input.MoveInput.magnitude > ZeroF )
        {
            if (_currentMoveSpeed < _parameters.maxMoveSpeed)
            {
                _currentMoveSpeed += _parameters.speedIncrement;
            }
        }
        else
        {
            _currentMoveSpeed = _parameters.baseMoveSpeed;
        }
        
        Vector3 calculatedPlayerMovement = (new Vector3(_playerMoveInput.x * _currentMoveSpeed * _rigidbody.mass,
            _playerMoveInput.y * _rigidbody.mass,
            _playerMoveInput.z * _currentMoveSpeed * _rigidbody.mass));
        
        _playerMoveInput = calculatedPlayerMovement;
        _cameraRelativeMovement = ConvertToCameraSpace(_playerMoveInput);
        PlayerSlope();
    }

    public void PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = _cameraRelativeMovement;

        if (_groundCheck.IsGrounded)
        {
            Vector3 localGroundCheckHitNormal = _groundCheck.GroundCheckHit.normal;
            float groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, _rigidbody.transform.up);
            if (groundSlopeAngle != 0f)
            {
                Quaternion slopeAngleRotation = Quaternion.FromToRotation(_rigidbody.transform.up, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation *  calculatedPlayerMovement;
            }
        }
        _cameraRelativeMovement = calculatedPlayerMovement;
        
    }
    
    public void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _cameraRelativeMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = _cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (_input.MoveInput.magnitude > ZeroF && positionToLookAt != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _parameters.rotationSpeed * Time.deltaTime);
        }

    }
}
