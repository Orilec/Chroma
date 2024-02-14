using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera1;
    [SerializeField] private CinemachineVirtualCamera _camera2;
    private CinemachineBrain _cinemachineBrain;
    private void Awake()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _camera2.Priority = 100;
            _camera1.Priority = 1;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _camera1.Priority = 100;
            _camera2.Priority = 1;
        }
    }
}
