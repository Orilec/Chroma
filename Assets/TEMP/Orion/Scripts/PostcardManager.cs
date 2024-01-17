using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PostcardManager : MonoBehaviour
{
    [SerializeField] private List<Postcard> _postcards;
    [SerializeField] private Postcard _postcardPrefab;
    [SerializeField] private Transform _postcardContainer;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float _postcardAnimationOffset = 50f;
    [SerializeField] private float _postcardAnimationTime = 0.1f;
    
    private Postcard _selectedPostcard, _lastSelected;
    private int _currentAddedIndex, _lastSelectedIndex;
    
    public float PostcardAnimationOffset { get { return _postcardAnimationOffset; } }
    public float PostcardAnimationTime { get { return _postcardAnimationTime; } }
    public Postcard SelectedPostcard { get { return _selectedPostcard; } set { _selectedPostcard = value; } }
    public Postcard LastSelectedPostcard { get { return _lastSelected; } set { _lastSelected = value; } }
    public int LastSelectedIndex { get { return _lastSelectedIndex; } set { _lastSelectedIndex = value; } }
    public List<Postcard> Postcards { get { return _postcards; } }

    private void OnEnable()
    {
        if(_currentAddedIndex > 0) EventSystem.current.SetSelectedGameObject(_postcards[0].gameObject);
    }

    private void Awake()
    {
        _playerEvents.CardCollected.AddListener(AddNewPostCard);
    }

    private void AddNewPostCard(Sprite sprite)
    {
        if (_currentAddedIndex < _postcards.Count)
        {
            _postcards[_currentAddedIndex].InitCard(sprite, this);
            EventSystem.current.SetSelectedGameObject(_postcards[_currentAddedIndex].gameObject);
            _currentAddedIndex++;
        }
    }
    
    private void HandleCardSelection(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            int newIndex = _lastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, _postcards.Count - 1);
            EventSystem.current.SetSelectedGameObject(_postcards[_lastSelectedIndex].gameObject);
        }
    }
}
