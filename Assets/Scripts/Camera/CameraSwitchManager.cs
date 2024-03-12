using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraSwitchManager : MonoBehaviour
{
    
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private CinemachineVirtualCameraBase _flCamera;

    private List<CinemachineVirtualCameraBase> _possibleCameras;
    private CinemachineVirtualCameraBase _currentCamera;

    private void Awake()
    {
        _possibleCameras = new List<CinemachineVirtualCameraBase>();
        _narrativeEvents.TriggerCamera.AddListener(OnTriggerCameraZone);
        _currentCamera = _flCamera;
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


