using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PostcardManager : MonoBehaviour
{
    [SerializeField] private List<Postcard> _postcards;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float _postcardAnimationOffset = 50f;
    [SerializeField] private float _postcardAnimationTime = 0.1f;
    [SerializeField] private RectTransform _postcardCloseUpTransform;
    [SerializeField] private InputReader _input;
    [SerializeField] private Button _backButton;
    
    private Postcard _selectedPostcard, _lastSelected, _viewedPostcard;
    private int _currentAddedIndex, _lastSelectedIndex;
    private List<Postcard> _selectablePostcards;
    private bool _clickWasPressedLastFrame, _isViewingPostcard, _backWasPressedLastFrame;
    private Image _postcardCloseUpImage;
    
    public float PostcardAnimationOffset { get { return _postcardAnimationOffset; } }
    public bool IsViewingPostcard { get { return _isViewingPostcard; } }
    public float PostcardAnimationTime { get { return _postcardAnimationTime; } }
    public Postcard SelectedPostcard { get { return _selectedPostcard; } set { _selectedPostcard = value; } }
    public Postcard LastSelectedPostcard { get { return _lastSelected; } set { _lastSelected = value; } }
    public int LastSelectedIndex { get { return _lastSelectedIndex; } set { _lastSelectedIndex = value; } }
    public List<Postcard> SelectablePostcards { get { return _selectablePostcards; } }

    private void OnEnable()
    {
        if(_currentAddedIndex > 0) EventSystem.current.SetSelectedGameObject(_selectablePostcards[0].gameObject);
    }

    private void Awake()
    {
        _postcardCloseUpImage = _postcardCloseUpTransform.GetComponent<Image>();
        _playerEvents.CardCollected.AddListener(AddNewPostCard);
        _selectablePostcards = new List<Postcard>();
    }
    
    private void Update()
    {
        if (_input.NavigateInput.x >= 0.95)
        {
            HandleCardSelection(1);
        }
        else if (_input.NavigateInput.x <= -0.95)
        {
            HandleCardSelection(-1);
        }

        if (_input.ClickIsPressed && _selectedPostcard != null && !_clickWasPressedLastFrame)
        {
            _selectedPostcard.CardButton.onClick.Invoke();
        }

        if (_input.BackIsPressed && !_backWasPressedLastFrame && _isViewingPostcard)
        {
            HideShownPostcard();
        }

        _clickWasPressedLastFrame = _input.ClickIsPressed;
        _backWasPressedLastFrame = _input.BackIsPressed;
    }

    private void AddNewPostCard(Sprite sprite)
    {
        if (_currentAddedIndex < _postcards.Count)
        {
            var addedCard = _postcards[_currentAddedIndex];
            _selectablePostcards.Add(addedCard);
            addedCard.InitCard(sprite, this);
            EventSystem.current.SetSelectedGameObject(addedCard.gameObject);
            _currentAddedIndex++;
        }
    }
    
    private void HandleCardSelection(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            int newIndex = _lastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, _selectablePostcards.Count - 1);
            EventSystem.current.SetSelectedGameObject(_selectablePostcards[newIndex].gameObject);
            _selectedPostcard = _selectablePostcards[newIndex];
        }
    }

    public void ShowPostcard(Postcard card, bool show)
    {
        _backButton.gameObject.SetActive(show);
        _isViewingPostcard = show;
        card.CardImage.enabled = !show;
        _postcardCloseUpImage.sprite = card.CardImage.sprite;
        _postcardCloseUpImage.enabled = show;
        if(show) _viewedPostcard = card;
    }

    public void HideShownPostcard()
    {
        ShowPostcard(_viewedPostcard, false);
    }
}
