using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[CreateAssetMenu]
public class DioramaInputReader : ScriptableObject, DioramaInputActions.IDioramaControlActions
{

    public event UnityAction <Vector2, bool> Look = delegate{ };
    public event UnityAction  EnableMouseControlCamera = delegate{ };
    public event UnityAction  DisableMouseControlCamera = delegate{ };

    private DioramaInputActions CurrentDioramaInputActions;

    private void OnEnable()
    {
        if (CurrentDioramaInputActions == null)
        {
            CurrentDioramaInputActions = new DioramaInputActions();
            CurrentDioramaInputActions.DioramaControl.SetCallbacks(this);
        }
    }

    public void EnableInputActions()
    {
        CurrentDioramaInputActions.Enable();
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                EnableMouseControlCamera.Invoke();
                break;
            case InputActionPhase.Canceled:
                DisableMouseControlCamera.Invoke();
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    
}
