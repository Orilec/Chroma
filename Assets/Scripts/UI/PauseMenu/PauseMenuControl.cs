using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private InputReader _input;
    [SerializeField] private Button _restartButton;
    private SceneLoader _sceneLoader;
    private Transform _menuTransform;
    private bool _isDisplayed, _displayedWasPressedLastFrame;

    private void Awake()
    {
        _menuTransform = transform.GetChild(0);
        _sceneLoader = FindObjectOfType<SceneLoader>();
        if(_sceneLoader != null ) _restartButton.onClick.AddListener(_sceneLoader.Reload);
    }

    private void Update()
    {
        if (_input.DisplayMenu && !_displayedWasPressedLastFrame)
        {
            if(_isDisplayed) HideMenu();
            else if(!_isDisplayed && !PauseControl.IsPaused) DisplayMenu();
        }
        _displayedWasPressedLastFrame = _input.DisplayMenu;
    }

    public void HideMenu()
    {
        _input.DisableUIControl();
        _input.EnableCharacterControl();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _menuTransform.gameObject.SetActive(false);
        _isDisplayed = false;
        _playerEvents.PauseGame.Invoke(false);
    }

    public void DisplayMenu()
    {
        _input.EnableUIControl();
        _input.DisableCharacterControl();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _menuTransform.gameObject.SetActive(true);
        _isDisplayed = true;
        _playerEvents.PauseGame.Invoke(true);
    }
}
