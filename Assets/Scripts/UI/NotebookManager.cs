using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private InputReader _input;
    private Transform _notebookTransform;
    private bool _isDisplayed, _displayedWasPressedLastFrame;

    private void Awake()
    {
        _notebookTransform = transform.GetChild(0); 
    }

    private void Update()
    {
        if (_input.DisplayNotebook && !_displayedWasPressedLastFrame)
        {
            DisplayNotebook();
        }
        _displayedWasPressedLastFrame = _input.DisplayNotebook;
    }

    private void DisplayNotebook()
    {
            _notebookTransform.gameObject.SetActive(!_isDisplayed);
            _isDisplayed = !_isDisplayed;
            _playerEvents.PauseGame.Invoke(_isDisplayed);
    }
}
