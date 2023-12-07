using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickInteraction : MonoBehaviour
{
    [SerializeField] private DioramaInputReader _input;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _layer;
    private void OnEnable()
    {
        _input.MouseClick += OnMouseClick;
    }
    
    private void OnMouseClick()
    {
        Physics.Raycast(_mainCam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, 100000f, _layer);
        if (hit.transform != null)
        {
            var colorable = hit.transform.GetComponent<ColorableObject>();
            if (colorable != null)
            {
                colorable.SetObjectActive();
            }
        }
    }
}
