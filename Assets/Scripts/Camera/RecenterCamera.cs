using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RecenterCamera : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    private CinemachineFreeLook _camera;
    private bool _isRecentering;
    private void OnEnable()
    {
        _camera = GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Start()
    {
        StartCoroutine(Recenter());
    }

    private void Update()
    {
        if (_input.RecenterCameraIsPressed && !_isRecentering)
        {
            
        }
    }

    private IEnumerator Recenter()
    {
        _isRecentering = true;
        _camera.m_RecenterToTargetHeading.m_enabled = true;
        _camera.m_YAxisRecentering.m_enabled = true;
        yield return new WaitForSeconds(_camera.m_RecenterToTargetHeading.m_RecenteringTime * 2f);
        _camera.m_RecenterToTargetHeading.m_enabled = false;
        _camera.m_YAxisRecentering.m_enabled = false;
        _isRecentering = false;
    }
}
