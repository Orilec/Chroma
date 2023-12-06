using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DioramaCameraBehavior : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] DioramaInputReader _input;
    [SerializeField] CinemachineFreeLook _freeLookCamera;
   
    [Header("Settings")] 
    [SerializeField] float _XspeedMultiplier = 1f;
    [SerializeField] float _YspeedMultiplier = 1f;
    [SerializeField] private float _minXValue = 0f;
    [SerializeField] private float _maxXValue = 180f;
           
    bool isRMBPressed;
    bool cameraMovementLock;
   
    void OnEnable() {
        _input.EnableMouseControlCamera += OnEnableMouseControlCamera;
        _input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        _input.Look += OnLook;
    }
    
    void OnDisable() {
        _input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
        _input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        _input.Look -= OnLook;
    }

    private void Start()
    {
        _input.EnableInputActions();
    }

    private void OnEnableMouseControlCamera()
    {
        isRMBPressed = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        StartCoroutine(DisableMouseForFrame());
    }

    private void OnDisableMouseControlCamera()
    {
        isRMBPressed = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
    {
        if (cameraMovementLock) return;
        if (isDeviceMouse && !isRMBPressed) return;
        float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

        
        
        var newXValue = _freeLookCamera.m_XAxis.Value + cameraMovement.x * _XspeedMultiplier * deviceMultiplier;
        _freeLookCamera.m_XAxis.Value = newXValue;

        _freeLookCamera.m_YAxis.Value += cameraMovement.y * _YspeedMultiplier * deviceMultiplier;
    }
    
    IEnumerator DisableMouseForFrame() {
        cameraMovementLock = true;
        yield return new WaitForEndOfFrame();
        cameraMovementLock = false;
    }
}
