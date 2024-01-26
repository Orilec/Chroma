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
    [SerializeField] private InputReader _inputReader;
    
    private Postcard _selectedPostcard, _lastSelected;
    private int _currentAddedIndex, _lastSelectedIndex;
    private List<Postcard> _selectablePostcards;
    private bool _clickWasPressedLastFrame;
    private Image _postcardCloseUpImage;
    
    public float PostcardAnimationOffset { get { return _postcardAnimationOffset; } }
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
        if (_inputReader.NavigateInput.x == 1)
        {
            HandleCardSelection(1);
        }
        else if (_inputReader.NavigateInput.x == -1)
        {
            HandleCardSelection(-1);
        }

        if (_inputReader.ClickIsPressed && _selectedPostcard != null && !_clickWasPressedLastFrame)
        {
            _selectedPostcard.CardButton.onClick.Invoke();
        }

        _clickWasPressedLastFrame = _inputReader.ClickIsPressed;
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

    public void ShowPostcard(Postcard card)
    {
        card.CardImage.enabled = false;
        _postcardCloseUpImage.sprite = card.CardImage.sprite;
        _postcardCloseUpImage.enabled = true;
    }
}
