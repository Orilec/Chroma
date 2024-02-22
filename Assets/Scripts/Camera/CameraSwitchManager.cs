using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitchManager : MonoBehaviour
{
    
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private CinemachineVirtualCamera _simpleFollowCamera;

    private List<CinemachineVirtualCamera> _possibleCameras;
    private CinemachineVirtualCamera _currentCamera;

    private void Awake()
    {
        _possibleCameras = new List<CinemachineVirtualCamera>();
        _narrativeEvents.TriggerCamera.AddListener(OnTriggerCameraZone);
        _currentCamera = _simpleFollowCamera;
    }

    private void OnTriggerCameraZone(CinemachineVirtualCamera camera, bool entering)
    {
        if (entering)
        {
            if (!_possibleCameras.Contains(camera)) _possibleCameras.Add(camera);

            if (_currentCamera == _simpleFollowCamera)
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
                SetNewCamera(_simpleFollowCamera);
            }
        }
    }
        private void SetNewCamera(CinemachineVirtualCamera camera)
        {
            if (_possibleCameras.Contains(camera)) _possibleCameras.Remove(camera);
            camera.Priority = 10;
            _currentCamera.Priority = 1;
            _currentCamera = camera;
            Debug.Log(_currentCamera);
        }
        
}


