using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private InputReader _input;
    [SerializeField] private UIEventsPublisher _uiEvents;
    [SerializeField] private FlippingNotebook _notebook;
    [SerializeField] private PostcardManager _postcardManager;
    [SerializeField] private RectTransform _postcardContainer;
    [SerializeField] private RectTransform _leftBookPages;
    [SerializeField] private RectTransform _rightBookPages;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private RectTransform _cueTransform;
    [SerializeField] private TextMeshProUGUI _textCue;
    private Transform _notebookTransform;
    private bool _isDisplayed, _displayedWasPressedLastFrame, _backWasPressedLastFrame, _isInPostcardSection;
    
    public int pagesToAdd;

    private void Awake()
    {
        _notebookTransform = transform.GetChild(0); 
        _uiEvents.NotebookFlipped.AddListener(HandleSectionDisplay);
        _narrativeEvents.CardCollected.AddListener(DisplayAddedCardCue);
        _narrativeEvents.AddingPages.AddListener(DisplayAddedPagesCue);
    }
    

    private void Start()
    {
        _input.DisableUIControl();
    }

    private void DisplayAddedCardCue(Sprite card)
    {
        StartCoroutine(DisplayCue("Carte postale ajoutée", 4f));
    }
    private void DisplayAddedPagesCue(int pages)
    {
        StartCoroutine(DisplayCue("Pages ajoutées", 4f));
        pagesToAdd += pages;
    }

    private IEnumerator DisplayCue(String text, float displayTime)
    {
        _textCue.text = text;
        _cueTransform.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(displayTime);
        _cueTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_input.DisplayNotebook && !_displayedWasPressedLastFrame)
        {
            if(_isDisplayed && !_notebook.IsFlipping) HideNotebook();
            else if(!_isDisplayed && !PauseControl.IsPaused) DisplayNotebook();
        }
        _displayedWasPressedLastFrame = _input.DisplayNotebook;
        
        if (_input.NavigateInput.x >= 0.95)
        {
            _notebook.FlipRightPage();
        }
        else if (_input.NavigateInput.x <= -0.95 && !_isInPostcardSection)
        {
            _notebook.FlipLeftPage();
        }
        
        if (_isDisplayed && _input.BackIsPressed && !_backWasPressedLastFrame && !_postcardManager.IsViewingPostcard)
        {
            HideNotebook();
        }

        _backWasPressedLastFrame = _input.BackIsPressed;
    }

    private void HideNotebook()
    {
        _input.DisableUIControl();
        _input.EnableCharacterControl();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _notebookTransform.gameObject.SetActive(false);
        _isDisplayed = false;
        _playerEvents.PauseGame.Invoke(false);
    }

    private void DisplayNotebook()
    {
        _input.EnableUIControl();
        _input.DisableCharacterControl();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        _notebookTransform.gameObject.SetActive(true);
        _isDisplayed = true;
        _playerEvents.PauseGame.Invoke(true);
    }

    private void HandleSectionDisplay(bool startOfBook, bool endOfBook)
    {
        if (startOfBook) {SectionDisplay(null, _leftBookPages, _leftButton,true);}
        else if(endOfBook) SectionDisplay(_postcardContainer, _rightBookPages, _rightButton, true);
        else
        {
            SectionDisplay(null, _leftBookPages, _leftButton,false);
            SectionDisplay(_postcardContainer, _rightBookPages, _rightButton, false);
            SectionDisplay(null,null, null, true);
        }

        _isInPostcardSection = endOfBook;
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
