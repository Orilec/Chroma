using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private InputReader _input;
    [SerializeField] private UIEventsPublisher _uiEvents;
    [SerializeField] private FlippingNotebook _notebook;
    [SerializeField] private RectTransform _postcardContainer;
    [SerializeField] private RectTransform _leftBookPages;
    [SerializeField] private RectTransform _rightBookPages;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    private Transform _notebookTransform;
    private bool _isDisplayed, _displayedWasPressedLastFrame;

    private void Awake()
    {
        _notebookTransform = transform.GetChild(0); 
        _uiEvents.NotebookFlipped.AddListener(HandleSectionDisplay);
    }

    private void Start()
    {
        _input.EnableUIControl();
        _input.DisableCharacterControl();
    }

    private void Update()
    {
        if (_input.DisplayNotebook && !_displayedWasPressedLastFrame)
        {
            DisplayNotebook();
        }
        _displayedWasPressedLastFrame = _input.DisplayNotebook;
        
        if (_input.NavigateInput.x >= 0.95)
        {
            _notebook.FlipRightPage();
        }
        else if (_input.NavigateInput.x <= -0.95)
        {
            _notebook.FlipLeftPage();
        }
    }

    private void DisplayNotebook()
    {
            _notebookTransform.gameObject.SetActive(!_isDisplayed);
            _isDisplayed = !_isDisplayed;
            _playerEvents.PauseGame.Invoke(_isDisplayed);
    }

    private void HandleSectionDisplay(bool startOfBook, bool endOfBook)
    {
        if (startOfBook) SectionDisplay(null, _leftBookPages, _leftButton,true);
        else if(endOfBook) SectionDisplay(_postcardContainer, _rightBookPages, _rightButton, true);
        else
        {
            SectionDisplay(null, _leftBookPages, _leftButton,false);
            SectionDisplay(_postcardContainer, _rightBookPages, _rightButton, false);
            SectionDisplay(null,null, null, true);
        }
    }

    public void HidePostcards()
    {
        _postcardContainer.gameObject.SetActive(false);
    }

    private void SectionDisplay(RectTransform section, RectTransform bookPages, Button flipButton, bool show)
    {
        if(section != null) section.gameObject.SetActive(show);
        if(bookPages != null) bookPages.gameObject.SetActive(!show);
        if(flipButton != null) flipButton.gameObject.SetActive(!show);
    }
}
