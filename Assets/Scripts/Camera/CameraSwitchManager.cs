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

    private void Awake()
    {
        _narrativeEvents.TriggerCamera.AddListener(OnTriggerCameraZone);
    }

    private void OnTriggerCameraZone(CinemachineVirtualCamera camera, bool entering)
    {

    }
    
    
}
