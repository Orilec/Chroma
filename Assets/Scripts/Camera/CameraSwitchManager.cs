using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraSwitchManager : MonoBehaviour
{
    
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private CinemachineVirtualCameraBase _flCamera;
    [SerializeField] private CinemachineInputProvider _cinemachineInput;

    private InputReader _input;
    private List<CinemachineVirtualCameraBase> _possibleCameras;
    private CinemachineVirtualCameraBase _currentCamera;



    private void Awake()
    {
        _possibleCameras = new List<CinemachineVirtualCameraBase>();
        _narrativeEvents.TriggerCamera.AddListener(OnTriggerCameraZone);
        _currentCamera = _flCamera;
    }

    private void Start()
    {
        _input = ChroManager.GetManager<InputReader>();
    }

    private void Update()
    {
        if (_cinemachineBrain.IsBlending)
        {
            _cinemachineInput.enabled = false;
        }
        else
        {
            _cinemachineInput.enabled = true;
        }
    }

    public bool IsFlCamera()
    {
        return _currentCamera == _flCamera;
    }

    private void OnTriggerCameraZone(CinemachineVirtualCamera camera, bool entering)
    {
        if (entering)
        {
            if (!_possibleCameras.Contains(camera)) _possibleCameras.Add(camera);

            if (_currentCamera == _flCamera)
            {
                SetNewCamera(camera);
            }
        }
        else
        {
            if (_possibleCameras.Count > 0)
            {
                SetNewCamera(_possibleCameras[0]);
            }
            else
            {
                SetNewCamera(_flCamera);
            }
        }
    }
        private void SetNewCamera(CinemachineVirtualCameraBase camera)
        {
            if (_possibleCameras.Contains(camera)) _possibleCameras.Remove(camera);
            camera.Priority = 10;
            _currentCamera.Priority = 1;
            _currentCamera = camera;
        }
        
}


