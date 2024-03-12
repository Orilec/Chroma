using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float  _movementSmoothTime = 0.1f;
    [SerializeField] private float  _verticalSmoothTime = 0.25f;
    private float _followSpeed = 1000;
    [SerializeField] private float _targetYOffset;
    
    private CinemachineFreeLook _flCamera;
    private InputReader _input;

    private float _targetY;
    private float _currentSmoothTime;
    private Vector3 _vel;
    private Camera _mainCam;

    private void OnEnable()
    {
        _playerEvents.LeavingGround.AddListener(OnLeavingGround);
        _flCamera = FindObjectOfType<CinemachineFreeLook>();
        _input = FindObjectOfType<InputReader>();
    }

    void Start()
    {
        _mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _targetTransform.position = new Vector3(_playerTransform.position.x,_playerTransform.position.y + _targetYOffset, _playerTransform.position.z);
    }

    private void OnLeavingGround()
    {
        _targetY = _playerTransform.position.y;
        _currentSmoothTime = _verticalSmoothTime;
    }
    
    
    void LateUpdate()
    {
        Vector3 viewPos = _mainCam.WorldToViewportPoint(_playerTransform.position + _playerController.Rigidbody.velocity * Time.deltaTime);
        
        if (viewPos.y > 0.95f || viewPos.y < 0.3f)
        {
            _targetY = _playerTransform.position.y;
            _currentSmoothTime = _movementSmoothTime;
        }
        if(_playerController.GroundCheck.IsGrounded)
        {
            _targetY = _playerTransform.position.y;
            _currentSmoothTime = _movementSmoothTime;
        }

        _targetTransform.rotation = _playerTransform.rotation;
        var desiredPosition = new Vector3(_playerTransform.position.x, _targetY + _targetYOffset, _playerTransform.position.z);
        _targetTransform.position = Vector3.SmoothDamp(_targetTransform.position, desiredPosition, ref _vel, _currentSmoothTime, _followSpeed);

        if (_input.MoveInput.magnitude > 0)
        {
            _flCamera.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            _flCamera.m_RecenterToTargetHeading.m_enabled = false;
        }
    }

    void RecenterCamera()
    {
        _flCamera.m_XAxis.Value = 0;
        _flCamera.m_YAxis.Value = 0.5f;
    }
}
